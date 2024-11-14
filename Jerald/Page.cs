using GorillaNetworking;
using System.Text;

namespace Jerald
{
    public abstract class Page
    {
        public abstract string PageName { get; }

        /// <summary> This will be called whenever a keyboard button is pressed, player event occures, or the UpdateText method is called.</summary>
        public abstract StringBuilder GetPageContent();

        internal string NormalizedPageName => PageName.Normalize().Trim().ToUpper();

        public delegate void OnKeyPressedMethod(GorillaKeyboardBindings key);
        /// <summary> When the page is active and a key is pressed.</summary>
        protected event OnKeyPressedMethod OnKeyPressed;

        public delegate void OnPageOpenedDelegate();
        /// <summary> When the page is selected.</summary>
        protected event OnPageOpenedDelegate OnPageOpened;

        public void UpdateContent()
        {
            GorillaComputer.instance.screenText.Text =
            GetPageContent().ToString().ToUpper();
        }

        internal void InvokeKeyStrokeEvent(GorillaKeyboardBindings button)
        {
            if (OnKeyPressed != null)
            {
                OnKeyPressed(button);
                return;
            }
            Main.Logger.LogWarning("Key event no defined for page " + PageName);
        }

        internal void InvokePageOpenedEvent()
        {
            if (OnPageOpened != null)
            {
                OnPageOpened();
                return;
            }
            Main.Logger.LogWarning("OnPageOpened event no defined for page " + PageName);
        }
    }
}
