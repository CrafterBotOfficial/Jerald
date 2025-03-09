using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using GorillaNetworking;
using HarmonyLib;
using Jerald;

[assembly: AutoRegister]

namespace Example
{
    [BepInPlugin("crafterbot.computer.example", "Computer Example", "1.0.1")]
    [BepInDependency("crafterbot.gorillatag.computer")]
    public class Main : BaseUnityPlugin
    {
        public static ManualLogSource Logger;
        public static ConfigFile Config;

        private void Awake()
        {
            Logger = base.Logger;
            Config = base.Config;
            Logger.LogInfo("Loaded");
        }

#if DUMP_DATA
        private void Start()
        {
            new Harmony(Info.Metadata.GUID).Patch(
                original: AccessTools.Method(typeof(GorillaKeyboardButton), "OnTriggerEnter"),
                postfix: new HarmonyMethod(typeof(DumpComputerInfo), "Begin"));
        }
#endif
    }
}
