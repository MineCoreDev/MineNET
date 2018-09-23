using MineNET.Entities.Players;

namespace MineNET.Events.PlayerEvents
{
    public class PlayerToggleSneakEventArgs : PlayerEventArgs, ICancelable
    {
        public bool IsSneaking { get; }

        public bool IsCancel { get; set; }

        public PlayerToggleSneakEventArgs(Player player, bool isSneaking) : base(player)
        {
            this.IsSneaking = isSneaking;
        }
    }
}
