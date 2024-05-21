using GorillaNetworking;
using System.Text;

namespace Jerald
{
    public abstract class Page
    {
        public abstract string GetPageName();

        /// <summary> This will be called whenever a keyboard button is pressed, player event occures, or the UpdateText method is called.</summary>
        public abstract StringBuilder GetPageContent();
    
        public void UpdateText()
        {
            Main.Logger.LogMessage("Updating custom text");
            GorillaComputer.instance.screenText.Text = GetPageContent().ToString().ToUpper();
        }
    }
}
