using MineNET.Data;
using MineNET.Entities.Players;

namespace MineNET.Events.PlayerEvents
{
    public class PlayerSkinChangeEventArgs : PlayerEventArgs, ICancelable
    {
        public Skin OldSkin { get; }
        public Skin NewSkin { get; set; }
        public bool IsCancel { get; set; }

        public PlayerSkinChangeEventArgs(Player player, Skin oldSkin, Skin newSkin) : base(player)
        {
            this.OldSkin = oldSkin;
            this.NewSkin = newSkin;
        }
    }
}
