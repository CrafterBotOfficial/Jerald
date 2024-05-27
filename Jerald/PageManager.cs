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
        private static int defaultPageCount;

        public static bool Initialized;


        public static void RegisterPages()
        {
            Main.Logger.LogMessage("Registering pages");
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            assemblies.Where(assembly => assembly != null && assembly.GetCustomAttribute<AutoRegisterAttribute>() != null).ForEach(async assembly =>
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

                            page.Index = Pages.Count;
                            Main.Logger.LogMessage($"{page.Index} - {Pages.Count}");
                            /*// force fix for the index unexpectly changing
                            unsafe
                            {
                                fixed (int* index = &page.Index)
                                {
                                    *index = 0;
                                }
                            }*/

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
            defaultPageCount = instance.stateStack.Count;

            foreach (var page in Pages)
            {
                enumBuilder.TryAddEnum(page.NormalizedTitle, typeof(PageManager).Assembly, out GorillaComputer.ComputerState newEnum);

                instance.OrderList.Add(new GorillaComputer.StateOrderItem(newEnum));
                instance.stateStack.Append(newEnum);
            }
            instance.SwitchState(GorillaComputer.ComputerState.Startup, false);
        }
    }
}
