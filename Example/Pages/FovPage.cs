using Jerald;
using System.Text;
using UnityEngine;

namespace Example.Pages
{
    // this page will not appear on the screen
    // [AutoRegister]
    public class FovPage : Page
    {
        public override string PageTitle => "Fov";

        private Camera camera => GorillaTagger.Instance.thirdPersonCamera.GetComponentInChildren<Camera>();

        public FovPage()
        {
            OnKeyPressed += (key) =>
            {
                Main.Logger.LogDebug("Pressed " + key.characterString);

                if (key.Binding == GorillaNetworking.GorillaKeyboardBindings.delete)
                {
                    camera.fieldOfView -= 10;
                    return;
                }

                if (!int.TryParse(key.characterString.Trim(), out int result))
                {
                    // not a number
                    return;
                }
                camera.fieldOfView += result * 10;
                UpdateContent();
            };
        }

        public override StringBuilder GetPageContent()
        {
            var stringBuilder = new StringBuilder()
                .Append("Adject the desktop views fov by changing the number below. This will not affect your headsets fov, only the specator camera.")
                .AppendLine($"\n Current fov: {camera.fieldOfView}");
            return stringBuilder;
        }
    }
}
