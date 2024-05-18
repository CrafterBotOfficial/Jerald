using GorillaNetworking;
using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;

namespace Jerald.Patches
{
    [HarmonyPatch(typeof(GorillaComputer))]
    public static class GorillaComputerPatches
    {
#if DEBUG
        [HarmonyDebug]
#endif
        [HarmonyPatch("UpdateScreen")]
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> UpdateScreen_Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator iLGenerator)
        {
            var codes = instructions.ToList();
            var switchStatementJumps = codes.Find(code => code.opcode == OpCodes.Switch).operand as IEnumerable<Label> ?? throw new System.Exception("Failed to find switch operator or parse its operand.");
            var endSwitchJump = codes[codes.FindIndex(code => code.opcode == OpCodes.Call && code.operand == AccessTools.Method(typeof(GorillaComputer), "UpdateGameModeText")) - 1];

            foreach (var page in PageManager.Pages)
            {
                Main.Logger.LogDebug("Adding MSIL instruction for " + page.GetPageName());

                // Inject switch block
                var blockStart = new CodeInstruction(OpCodes.Ldarg_0);
                blockStart.labels = [iLGenerator.DefineLabel()];
                codes.InsertRange(GetSwitchAppendPoint(codes), [
                    blockStart,
                    new CodeInstruction(OpCodes.Call, SymbolExtensions.GetMethodInfo(() => page.UpdateText())),
                    new CodeInstruction(OpCodes.Br_S, endSwitchJump.labels.First()),
                ]);

                switchStatementJumps.AddItem(blockStart.labels[0]);
            }

            return codes;
        }

        private static int GetSwitchAppendPoint(List<CodeInstruction> instructions)
        {
            // TODO impliment
            return 60;
        }
    }
}
