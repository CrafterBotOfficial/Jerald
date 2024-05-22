using GorillaNetworking;
using Nautilus.Handlers;
using System.Collections.Generic;
using System.Linq;

namespace Jerald
{
    public static class PageManager
    {
        public static List<Page> Pages = [new Testing.ExamplePage()];
        private static int defaultPageCount;

       
        public static void InjectPagesToEnum()
        {
            var enumBuilder = new EnumBuilder<GorillaComputer.ComputerState>();
            var instance = GorillaComputer.instance;
            defaultPageCount = instance.stateStack.Count;

            int count = 0;
            foreach (var page in Pages)
            {
                enumBuilder.TryAddEnum(page.NormalizedTitle, typeof(PageManager).Assembly, out GorillaComputer.ComputerState newEnum);

                instance.OrderList.Add(new GorillaComputer.StateOrderItem(newEnum));
                instance.stateStack.Push(newEnum);
                count++;
            }
        }
    }
}
