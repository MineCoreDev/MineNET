using MineNET.Entities.Players;
using MineNET.Inventories;

namespace MineNET.Events.InventoryEvents
{
    public class InventoryOpenEventArgs : InventoryEventArgs, ICancellable
    {
        public Player Player { get; set; }

        public bool IsCancel { get; set; }

        public InventoryOpenEventArgs(Inventory inventory, Player player) : base(inventory)
        {
            this.Player = player;
        }
    }
}
