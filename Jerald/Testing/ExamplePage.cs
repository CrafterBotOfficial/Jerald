using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jerald.Testing
{
    public class ExamplePage : Page
    {
        public override StringBuilder GetPageContent()
        {
            return new StringBuilder ("Im crying rn bc MSIL touched my doodle");
        }

        public override string GetPageName()
        {
            return "bruh";
        }
    }
}
