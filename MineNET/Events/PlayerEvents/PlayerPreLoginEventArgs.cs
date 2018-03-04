using MineNET.Entities;

namespace MineNET.Events.PlayerEvents
{
    public class PlayerPreLoginEventArgs : PlayerEventArgs, ICancellable
    {
        public string KickMessage { get; set; }

        public PlayerPreLoginEventArgs(Player player, string message) : base(player)
        {
            this.KickMessage = message;
        }

        public bool IsCancel { get; set; } = false;
    }
}
