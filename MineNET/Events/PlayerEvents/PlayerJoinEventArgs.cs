using MineNET.Entities.Players;

namespace MineNET.Events.PlayerEvents
{
    public class PlayerJoinEventArgs : PlayerEventArgs
    {
        public string JoinMessage { get; set; }

        public PlayerJoinEventArgs(Player player, string joinMessage) : base(player)
        {
            this.JoinMessage = joinMessage;
        }
    }
}
