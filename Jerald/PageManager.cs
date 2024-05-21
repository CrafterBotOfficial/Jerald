using GorillaNetworking;
using Nautilus.Handlers;
using System.Collections.Generic;

namespace Jerald
{
    internal static class PageManager
    {
        public static List<Page> Pages = [new Testing.ExamplePage(), new Testing.TestPage()];

        public static void InjectPagesToEnum()
        {
            var enumBuilder = new EnumBuilder<GorillaComputer.ComputerState>();

            int count = 0;
            foreach (var page in Pages)
            {
                enumBuilder.TryAddEnum(page.GetPageName(), typeof(PageManager).Assembly, out GorillaComputer.ComputerState value);
                GorillaComputer.instance.OrderList.Add(new GorillaComputer.StateOrderItem(value));
                count++;
            }
        }
    }
}
