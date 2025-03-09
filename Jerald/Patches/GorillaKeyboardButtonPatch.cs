using GorillaNetworking;
using HarmonyLib;
using UnityEngine;

namespace Jerald.Patches
{
    [HarmonyPatch(typeof(GorillaKeyboardButton), "OnButtonPressedEvent")]
    public static class GorillaKeyboardButtonPatch
    {
        private static void Postfix(GorillaKeyboardButton __instance)
        {
            if (PageManager.GetPage() is not Page page)
            {
                return;
            }
            if (__instance.characterString == "down" || __instance.characterString == "up" && __instance.functionKey) return;
            Main.Log("Keypress detected", BepInEx.Logging.LogLevel.Debug);
            page.InvokeKeyStroke(__instance);
        }
    }
}