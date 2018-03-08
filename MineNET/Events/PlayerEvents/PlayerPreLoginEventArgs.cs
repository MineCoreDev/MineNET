using MineNET.Entities.Players;

namespace MineNET.Events.PlayerEvents
{
    public class PlayerPreLoginEventArgs : PlayerEventArgs, ICancellable
    {
        public string KickMessage { get; set; }

        public bool IsCancel { get; set; }

        public PlayerPreLoginEventArgs(Player player, string message) : base(player)
        {
            this.KickMessage = message;
        }
    }
}
