using MineNET.Entities.Players;

namespace MineNET.Events.PlayerEvents
{
    public class PlayerJoinEventArgs : PlayerEventArgs, ICancelable
    {
        public string JoinMessage { get; set; }

        public string KickMessage { get; set; }

        public bool IsCancel { get; set; }

        public PlayerJoinEventArgs(Player player, string joinMessage, string kickMessage) : base(player)
        {
            this.JoinMessage = joinMessage;
            this.KickMessage = kickMessage;
        }
    }
}
