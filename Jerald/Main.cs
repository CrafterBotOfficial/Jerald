using BepInEx;
using BepInEx.Logging;
using GorillaNetworking;
using Jerald;
using System.Collections.Generic;

[assembly: AutoRegister]

namespace Jerald
{
    [BepInPlugin("crafterbot.gorillatag.computer", "Jerald", "1.0.0")]
    public class Main : BaseUnityPlugin
    {
        public static ManualLogSource Logger;

        // for some reason the game crashes if the page updatecontent method calls any abstractions 
        // so Im doin this to ensure its on the main thread and such
        internal static Queue<int> PageQueue = new();

        private void Awake()
        {
            Logger = base.Logger;
            PageManager.RegisterPages();
            HarmonyLib.Harmony.CreateAndPatchAll(typeof(Main).Assembly);
        }

        private void Update()
        {
            if (PageQueue.Count == 0)
                goto VerifyState;
            var pageIndex = PageQueue.Dequeue();
            Main.Logger.LogMessage(pageIndex);
            GorillaComputer.instance.screenText.Text =
            PageManager.Pages[pageIndex].GetPageContent().ToString().ToUpper();

        VerifyState:
            if (PageManager.Pages.Count == 0)
            {
                Logger.LogWarning("No custom pages found, disabling mod.");
                enabled = false;
            }
        }
    }
}