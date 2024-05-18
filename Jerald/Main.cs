using BepInEx;
using BepInEx.Logging;

namespace Jerald
{
    [BepInPlugin("crafterbot.gorillatag.computer", "Jerald", "1.0.0")]
    public class Main : BaseUnityPlugin
    {
        public static ManualLogSource Logger;

        private void Awake()
        {
            Logger = base.Logger;
            HarmonyLib.Harmony.CreateAndPatchAll(typeof(Main).Assembly);
        }
    }
}