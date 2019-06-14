using MineNET.Entities.Players;
using MineNET.Items;

namespace MineNET.Events.PlayerEvents
{
    public class PlayerItemHeldEventArgs : PlayerEventArgs, ICancelable
    {
        public Item Item { get; }
        public int OldHotbar { get; }
        public int NewHotbar { get; set; }
        public bool IsCancel { get; set; }

        public PlayerItemHeldEventArgs(Player player, Item item, int oldHotbar, int newHotbar) : base(player)
        {
            this.Item = item;
            this.OldHotbar = oldHotbar;
            this.NewHotbar = newHotbar;
        }
    }
}
