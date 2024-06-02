using Jerald;
using System.Text;

namespace Example.Pages
{
    [AutoRegister] // Required 
    public class PlayerListPage : Page
    {
        public override string PageTitle => "Players"; // The text that will be displayed in the function select screen

        // Called every second by the base game, or when base.UpdateContent() is called
        public override StringBuilder GetPageContent()
        {
            var stringBuilder = new StringBuilder();
            if (NetworkSystem.Instance is NetworkSystem networkSystem && networkSystem.InRoom)
                goto InRoom;
            return stringBuilder.Append("You are not currently in room.");

        InRoom:
            stringBuilder.Append($"Players {networkSystem.RoomPlayerCount}/10");
            networkSystem.AllNetPlayers.ForEach(player => stringBuilder.AppendLine(player.NickName));
            return stringBuilder;
        }
    }
}