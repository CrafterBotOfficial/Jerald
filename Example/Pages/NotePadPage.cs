using GorillaNetworking;
using Jerald;
using System.Text;

namespace Example.Pages
{
    [AutoRegister]
    public class NotePadPage : Page
    {
        public override string PageTitle => "Note Pad";

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
                    case GorillaKeyboardBindings.option2 | GorillaKeyboardBindings.option3 | GorillaKeyboardBindings.down | GorillaKeyboardBindings.up:
                        // do nothing
                        break;
                    case GorillaKeyboardBindings.enter:
                        Main.Logger.LogMessage("Saving");
                        Configuration.PersistantNote.Value = text;
                        Main.Config.Save();
                        break;
                    case GorillaKeyboardBindings.option1:
                        text += " ";
                        break;
                    default:
                        text += key.characterString;
                        break;
                }
            };
        }

        public override StringBuilder GetPageContent()
        {
            return new StringBuilder("Write anything you want to remember below.\n")
                .AppendLine(text);
        }
    }
}
