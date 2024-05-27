using System.Text;

namespace Jerald
{
    public abstract class Page
    {
        public abstract string PageTitle { get; }

        /// <summary> This will be called whenever a keyboard button is pressed, player event occures, or the UpdateText method is called.</summary>
        public abstract StringBuilder GetPageContent();

        internal string NormalizedTitle => PageTitle.Normalize().Trim().ToUpper();
        internal int Index;

        public void UpdateContent()
        {
            Main.PageQueue.Enqueue(Index);
        }
    }
}
