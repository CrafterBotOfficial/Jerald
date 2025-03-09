using GorillaNetworking;

namespace Jerald
{
    public abstract class Page
    {
        public abstract string PageName { get; }

        /// <summary> The higher in the function list your page will be, higher number = early in the list.</summary>
        public virtual int Priority => 0;

        internal string NormalizedPageName => PageName.Normalize().Trim().ToUpper();

        #region Events

        public delegate void OnKeyPressedMethod(GorillaKeyboardButton key);
        /// <summary> When the page is active and a key is pressed.</summary>
        protected event OnKeyPressedMethod OnKeyPressed;

        public delegate void OnPageOpenedDelegate();
        /// <summary> When the page is selected.</summary>
        protected event OnPageOpenedDelegate OnPageOpened;

        internal void InvokeKeyStroke(GorillaKeyboardButton button)
        {
            OnKeyPressed?.Invoke(button);
        }

        internal void InvokePageOpenedEvent()
        {
            OnPageOpened?.Invoke();
        }

        #endregion

        /// <summary> If blank the page will say loading.</summary>
        public virtual string GetContent() { return ""; }

        public void UpdateContent()
        {
            string content = GetContent();
            if (content.IsNullOrEmpty()) content = "Loading...";
            GorillaComputer.instance.screenText.Text = content.ToUpper();
        }


        #region Deprecated

        // <summary> This will be called whenever a keyboard button is pressed, player event occures, or the UpdateText method is called.</summary>
        // [Obsolete("Use GetContent() instead")]
        // public abstract StringBuilder GetPageContent();

        #endregion
    }
}
