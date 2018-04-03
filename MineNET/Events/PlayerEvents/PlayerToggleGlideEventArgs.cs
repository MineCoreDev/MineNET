using MineNET.Entities.Players;

namespace MineNET.Events.PlayerEvents
{
    public class PlayerToggleGlideEventArgs : PlayerEventArgs, ICancellable
    {
        public bool IsGliding { get; }

        public bool IsCancel { get; set; }

        public PlayerToggleGlideEventArgs(Player player, bool isGliding) : base(player)
        {
            this.IsGliding = isGliding;
        }
    }
}
