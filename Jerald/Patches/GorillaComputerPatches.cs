using GorillaNetworking;
using HarmonyLib;
using System;
using System.Linq;
using System.Text;
using UnityEngine;

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
            __instance.FunctionNames.AddRange(PageManager.Pages.Select(page => page.NormalizedPageName));
            foreach (var text in GameObject.FindObjectsOfType<UnityEngine.UI.Text>())
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
            const int itemsPerPage = 11;
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
            if (maxPages != currentPage) stringBuilder.AppendLine("...");
            __instance.functionSelectText.Text = stringBuilder.ToString();

            return false;
        }


        [HarmonyPatch("UpdateScreen")]
        [HarmonyPostfix]
        private static void UpdateScreen_Postfix()
        {
            if (PageManager.Initialized && PageManager.GetPage() is Page page)
            {
                page.UpdateContent();
            }
        }
        
        [HarmonyPatch("PressButton")]
        [HarmonyPrefix]
        private static void PressButtonPrefix(GorillaComputer __instance, GorillaKeyboardBindings buttonPressed)
        {
            if (buttonPressed == GorillaKeyboardBindings.up || buttonPressed == GorillaKeyboardBindings.down) return;
            Main.Logger.LogDebug("Keypress detected");
            PageManager.GetPage()?.InvokeKeyStrokeEvent(buttonPressed);
        }
    }
}
