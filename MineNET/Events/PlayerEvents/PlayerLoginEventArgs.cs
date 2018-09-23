using MineNET.Entities.Players;

namespace MineNET.Events.PlayerEvents
{
    public class PlayerLoginEventArgs : PlayerEventArgs, ICancelable
    {
        public string KickMessage { get; set; } = "";

        public bool IsCancel { get; set; }

        public PlayerLoginEventArgs(Player player) : base(player)
        {

        }
    }
}
