using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jerald.Testing
{
    public class ExamplePage : Page
    {
        public override string PageTitle { get => "Example"; }

        public override StringBuilder GetPageContent()
        {
            return new StringBuilder("Hello world, this is just an example.");
        }
    }
}
