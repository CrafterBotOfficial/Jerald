using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Jerald.Pages
{
    [AutoRegister]
    public class ModPage : Page
    {
        public override string PageName => "Jerald";
        public override int Priority => -1;
        // public const string ModOutdatedWarning = "<color=red>There is a new update aviable, note using older versions puts you at a higher risk of getting banned.";

        private string pageContents;

        public ModPage()
        {
            var builder = new StringBuilder("Jerald v" + Main.Version)
                .AppendLine($"Pages: [{PageManager.Pages.Count + PageManager.DefaultPageCount}]");
            pageContents = builder.ToString();
        }

        public override string GetContent()
        {
            return pageContents;
        }
    }
}
