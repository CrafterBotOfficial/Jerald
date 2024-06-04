using GorillaNetworking;
using HarmonyLib;
using UnityEngine;

namespace Jerald.Patches
{
    [HarmonyPatch(typeof(GorillaKeyboardButton), "OnTriggerEnter")]
    public static class GorillaKeyboardButtonPatch
    {
        private static void Postfix(GorillaKeyboardButton __instance, Collider collider)
        {
            if (!collider.TryGetComponent(out GorillaTriggerColliderHandIndicator indicator) || PageManager.GetPage() is not Page page)
            {
                return;
            }
            if (__instance.characterString == "down" || __instance.characterString == "up" && __instance.functionKey) return;
            Main.Logger.LogDebug("Keypress detected");
            page.InvokeKeyStrokeEvent(__instance);
        }
    }
}
