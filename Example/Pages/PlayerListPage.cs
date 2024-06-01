using Jerald;
using System.Text;

namespace Example.Pages
{
    [AutoRegister]
    public class PlayerListPage : Page
    {
        public override string PageTitle => "Players";

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
