using MineNET.Entities.Players;

namespace MineNET.Events.PlayerEvents
{
    public class PlayerPreLoginEventArgs : PlayerEventArgs, ICancelable
    {
        public string KickMessage { get; set; } = "";

        public bool IsCancel { get; set; }

        public PlayerPreLoginEventArgs(Player player) : base(player)
        {

        }
    }
}
