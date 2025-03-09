using BepInEx;
using BepInEx.Logging;

namespace Jerald
{
    [BepInPlugin("crafterbot.gorillatag.computer", "Jerald", Main.Version)]
    public class Main : BaseUnityPlugin
    {
        public const string Version = "1.0.1";
        private static Main instance;

        private void Awake()
        {
            instance = this;
            HarmonyLib.Harmony.CreateAndPatchAll(typeof(Main).Assembly);
        }

        public static void Log(object message, LogLevel level = LogLevel.Info)
        {
            instance?.Logger.Log(level, message);
        }
    }
}