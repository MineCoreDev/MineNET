using MineNET.Entities.Players;

namespace MineNET.Events.PlayerEvents
{
    public class PlayerLoginEventArgs : PlayerEventArgs, ICancellable
    {
        public string KickMessage { get; set; }

        public bool IsCancel { get; set; }

        public PlayerLoginEventArgs(Player player, string kickMessage) : base(player)
        {
            this.KickMessage = kickMessage;
        }
    }
}
