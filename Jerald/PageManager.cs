using GorillaNetworking;
using Nautilus.Handlers;
using System.Collections.Generic;

namespace Jerald
{
    internal static class PageManager
    {
        public static List<Page> Pages = [];

        public static void InjectPagesToEnum()
        {
            var enumBuilder = new EnumBuilder<GorillaComputer.ComputerState>();
            var instance = GorillaComputer.instance;

            int count = 0;
            foreach (var page in Pages)
            {
                enumBuilder.TryAddEnum(page.PageTitle, typeof(PageManager).Assembly, out GorillaComputer.ComputerState newEnum);
                
                instance.OrderList.Add(new GorillaComputer.StateOrderItem(newEnum));
                instance.stateStack.Push(newEnum);
                count++;
            }
        }
    }
}
