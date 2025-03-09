using BepInEx.Logging;
using GorillaNetworking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Jerald
{
    public static class PageManager
    {
        public static List<Page> Pages = [];
        public static int DefaultPageCount;

        public static bool Initialized;

        public static Page? GetPage()
        {
            int relativePageIndex = GorillaComputer.instance.currentStateIndex - DefaultPageCount;
            return (relativePageIndex < 0 || relativePageIndex >= Pages.Count) ? null : Pages[relativePageIndex];
        }

        public static void RegisterPages()
        {
            Main.Log("Registering custom pages");
            var assemblies = BepInEx.Bootstrap.Chainloader.PluginInfos.Values.Select(pluginInfo => pluginInfo.Instance.GetType().Assembly);
            assemblies.Where(assembly => assembly != null && assembly.GetCustomAttribute<AutoRegisterAttribute>() != null)
                      .ForEach(assembly =>
            {
                try
                {
                    var pages = assembly.GetTypes().Where(page => page.GetCustomAttribute<AutoRegisterAttribute>() != null).ToArray();
                    if (pages.Length > 0)
                    {
                        for (int i = 0; i < pages.Length; i++)
                        {
                            Main.Log("Found page", LogLevel.Debug);
                            var page = Activator.CreateInstance(pages[i]) as Page ?? throw new Exception("Failed to cast page type.");
                            Pages.Add(page);
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    Main.Log($"Failed to check {assembly.Location} {ex.Message}", LogLevel.Warning);
                }
            });
        }

        public static void AddPagesToComputer()
        {
            var instance = GorillaComputer.instance;
            int enumCount = Enum.GetValues(typeof(GorillaComputer.ComputerState)).Length;
            DefaultPageCount = instance.OrderList.Count;

            Pages = Pages.OrderBy(page => page.Priority).ToList();
            for (int i = 0; i < Pages.Count; i++)
            {
                var page = Pages[i];
                Main.Log("Adding page " + page.PageName, LogLevel.Message);
                var state = (GorillaComputer.ComputerState)enumCount + i;
                instance.OrderList.Add(new GorillaComputer.StateOrderItem(state));
            }
            instance._activeOrderList = instance.OrderList;
            Initialized = true;
            instance.SwitchState(GorillaComputer.ComputerState.Startup, false);
        }
    }
}
