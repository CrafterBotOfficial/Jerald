using System.Text;

namespace Jerald.Testing
{
    [AutoRegister]
    public class ExamplePage : Page
    {
        public override string PageTitle { get => "Example"; }

        public override StringBuilder GetPageContent()
        {
            return new StringBuilder("Hello world, this is just an example.");
        }
    }
}
