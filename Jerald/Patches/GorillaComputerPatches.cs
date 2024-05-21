using GorillaNetworking;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Jerald.Patches
{
    [HarmonyPatch(typeof(GorillaComputer))]
    public static class GorillaComputerPatches
    {
        [HarmonyPatch("Start")]
        [HarmonyPostfix]
        private static void Start_Postfix(GorillaComputer __instance)
        {
            __instance.FunctionsCount += PageManager.Pages.Count;
            __instance.FunctionNames.AddRange(PageManager.Pages.Select(page => page.GetPageName()));
            foreach (var text in GameObject.FindObjectsOfType<Text>())
            {
                if (text.gameObject.name.Contains("FunctionSelect"))
                    text.horizontalOverflow = UnityEngine.HorizontalWrapMode.Overflow;
            }
            PageManager.InjectPagesToEnum();
        }

        [HarmonyPatch("UpdateFunctionScreen")]
        [HarmonyPrefix]
        private static bool UpdateFunctionScreen_Prefix(GorillaComputer __instance)
        {
            const int itemsPerPage = 10;
            int indicatorIndex = __instance.GetStateIndex(__instance.currentState);
            int currentPage = indicatorIndex / itemsPerPage;
            var maxPages = Math.Ceiling((double)__instance.FunctionsCount / itemsPerPage) - 1;

            var stringBuilder = new StringBuilder($"Page {currentPage + 1}/{maxPages + 1}\n");
            for (int i = 0; i < itemsPerPage; i++)
            {
                int absoluteIndex = i + (currentPage * itemsPerPage);
                if (absoluteIndex >= __instance.FunctionsCount) break;

                var suffix = __instance.currentState == __instance.GetState(absoluteIndex) ? "<-" : "";
                stringBuilder.AppendLine(__instance.FunctionNames[absoluteIndex] + suffix);
            }
            if (maxPages != currentPage)
                stringBuilder.AppendLine("...");
            __instance.functionSelectText.Text = stringBuilder.ToString();

            Main.Logger.LogDebug($"Page:{currentPage} | max page:{maxPages} | indicatorIndex:{indicatorIndex}");
            return false;
        }


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
