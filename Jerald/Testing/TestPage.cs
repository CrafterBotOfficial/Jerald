#if DEBUG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jerald.Testing
{
    public class TestPage : Page
    {
        public override string GetPageName() => "Test Page";
        public override StringBuilder GetPageContent()
        {
            return new StringBuilder("Hello world");
        }
    }
}

#endif