using GorillaNetworking;
using Jerald;
using System.Text;

namespace Example.Pages
{
    [AutoRegister] // Tells Jerald to register this class
    public class NotePadPage : Page
    {
        public override string PageName => "Note Pad"; // The text that will be displayed in the function select screen

        private string text = Configuration.PersistantNote.Value;

        public NotePadPage()
        {
            base.OnKeyPressed += (key) =>
            {
                switch (key.Binding)
                {
                    case GorillaKeyboardBindings.delete:
                        text = text.Remove(text.Length - 1, 1);
                        break;
                    case GorillaKeyboardBindings.option2 | GorillaKeyboardBindings.option3:
                        // do nothing
                        break;
                    case GorillaKeyboardBindings.enter:
                        Main.Logger.LogMessage("Saving");
                        Configuration.PersistantNote.Value = text;
                        Main.Config.Save();
                        break;
                    case GorillaKeyboardBindings.option1:
                        text += " ";
                        UpdateContent();
                        break;
                    default:
                        text += key.characterString;
                        UpdateContent();
                        break;
                }
            };
        }

        public override StringBuilder GetPageContent()
        {
            return new StringBuilder("Write anything you want to remember below. Press enter to save\n")
                .AppendLine(text);
        }
    }
}