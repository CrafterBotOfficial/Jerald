using GorillaNetworking;
using Nautilus.Handlers;
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
            Main.Logger.LogMessage("Registering pages");
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
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
                            Main.Logger.LogDebug("Found page");
                            var page = Activator.CreateInstance(pages[i]) as Page ?? throw new Exception("Failed to cast page type.");
                            Pages.Add(page);
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    Main.Logger.LogWarning($"Failed to check {assembly.Location} {ex}");
                }
            });
        }

        public static void InjectPagesToEnum()
        {
            var enumBuilder = new EnumBuilder<GorillaComputer.ComputerState>();
            var instance = GorillaComputer.instance;
            DefaultPageCount = instance.OrderList.Count;

            foreach (var page in Pages)
            {
                enumBuilder.TryAddEnum(page.NormalizedTitle, typeof(PageManager).Assembly, out GorillaComputer.ComputerState newEnum);
                instance.OrderList.Add(new GorillaComputer.StateOrderItem(newEnum));
            }
            Initialized = true;
            instance.SwitchState(GorillaComputer.ComputerState.Startup, false);
        }
    }
}
