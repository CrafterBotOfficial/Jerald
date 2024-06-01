using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using Jerald;

[assembly: AutoRegister]

namespace Example
{
    [BepInPlugin("crafterbot.computer.example", "Computer Example", "1.0.0")]
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
    }
}
