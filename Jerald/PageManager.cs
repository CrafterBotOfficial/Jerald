using GorillaNetworking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine.Events;

namespace Jerald
{
    public static class PageManager
    {
        public static List<Page> Pages = [];

        public static bool Initialized;
        private static int DefaultPageCount;

        public static Page? GetPage()
        {
            Page returnPage = null;
            try
            {
                string name = GorillaComputer.instance.OrderList
                    .Find(x => x.State == GorillaComputer.instance.currentState).GetName();
                returnPage = Pages.First(x => x.PageName == name);
            }
            catch
            {
            }
            
            return returnPage;
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
                            var page = Activator.CreateInstance(pages[i], null) as Page ?? throw new Exception("Failed to cast page type.");
                            Pages.Add(page);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Main.Logger.LogWarning($"Failed to check {assembly.Location} {ex}");
                }
            });
        }

        public static void InjectPagesToEnum()
        {
            var instance = GorillaComputer.instance;
            DefaultPageCount = instance.OrderList.Count;

            foreach (var page in Pages)
            {
                int newPage =
                    Enum.GetNames(typeof(GorillaComputer.ComputerState)).ToList()
                        .IndexOf(Enum.GetNames(typeof(GorillaComputer.ComputerState)).Last()) + Pages.IndexOf(page) + 1;
                instance.OrderList.Add(new GorillaComputer.StateOrderItem((GorillaComputer.ComputerState)newPage, page.PageName));
                
            }
            Initialized = true;
        }
    }
}
