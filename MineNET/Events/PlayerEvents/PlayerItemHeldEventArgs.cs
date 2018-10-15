using MineNET.Entities.Players;
using MineNET.Items;

namespace MineNET.Events.PlayerEvents
{
    public class PlayerItemHeldEventArgs : PlayerEventArgs, ICancelable
    {
        public ItemStack Item { get; }
        public int OldHotbar { get; }
        public int NewHotbar { get; set; }
        public bool IsCancel { get; set; }

        public PlayerItemHeldEventArgs(Player player, ItemStack item, int oldHotbar, int newHotbar) : base(player)
        {
            this.Item = item;
            this.OldHotbar = oldHotbar;
            this.NewHotbar = newHotbar;
        }
    }
}
