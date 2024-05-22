using GorillaNetworking;
using System.Text;

namespace Jerald
{
    public abstract class Page
    {
        public abstract string PageTitle { get; }

        /// <summary> This will be called whenever a keyboard button is pressed, player event occures, or the UpdateText method is called.</summary>
        public abstract StringBuilder GetPageContent();

        internal string NormalizedTitle => PageTitle.Normalize().Trim().ToUpper();

        public void UpdateContent()
        {
            /*if (GorillaComputer.instance is not GorillaComputer computer || (int)computer.currentState != PageManager.Pages.IndexOf(this))
            {
                throw new Exception("Invalid state");
            }*/
            string content = GetPageContent().ToString().ToUpper();
            GorillaComputer.instance.screenText.Text = content;
        }
    }
}
