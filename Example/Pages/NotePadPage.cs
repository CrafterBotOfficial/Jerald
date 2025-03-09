using GorillaNetworking;
using Jerald;

namespace Example.Pages
{
    [AutoRegister] // Tells Jerald to register this class
    public class NotePadPage : Page
    {
        public override string PageName => "Notes"; // This will be displayed in the function select screen

        private string note = Configuration.PersistantNote.Value;
        private string pageContents;

        public NotePadPage()
        {
            base.OnKeyPressed += (key) =>
            {
                switch (key.Binding)
                {
                    case GorillaKeyboardBindings.delete:
                        note = note.Remove(note.Length - 1, 1);
                        break;
                    case GorillaKeyboardBindings.option2 | GorillaKeyboardBindings.option3 | GorillaKeyboardBindings.down | GorillaKeyboardBindings.up:
                        // do nothing
                        break;
                    case GorillaKeyboardBindings.enter:
                        Configuration.PersistantNote.Value = note;
                        Main.Config.Save();
                        break;
                    case GorillaKeyboardBindings.option1:
                        note += " ";
                        break;
                    default:
                        note += key.characterString;
                        break;
                }
                pageContents = "Write anything you want to remember below.\n" + note;
            };
        }

        public override string GetContent()
        {
            return pageContents;
        }
    }
}