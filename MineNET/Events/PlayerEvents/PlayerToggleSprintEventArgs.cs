using MineNET.Entities.Players;

namespace MineNET.Events.PlayerEvents
{
    public class PlayerToggleSprintEventArgs : PlayerEventArgs, ICancelable
    {
        public bool IsSprinting { get; }

        public bool IsCancel { get; set; }

        public PlayerToggleSprintEventArgs(Player player, bool isSprinting) : base(player)
        {
            this.IsSprinting = isSprinting;
        }
    }
}
