using GorillaNetworking;
using System;
using System.Text;

namespace Jerald
{
    public abstract class Page
    {
        public abstract string PageTitle { get; set; }

        /// <summary> This will be called whenever a keyboard button is pressed, player event occures, or the UpdateText method is called.</summary>
        public abstract StringBuilder GetPageContent();

        public void UpdateText()
        {
            Main.Logger.LogDebug("Updating custom text for " + PageTitle);
            GorillaComputer.instance.screenText.Text = GetPageContent().ToString().ToUpper();
        }
    }
}
