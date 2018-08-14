using MineNET.Entities.Players;
using MineNET.Inventories;

namespace MineNET.Events.InventoryEvents
{
    public class InventoryCloseEventArgs : InventoryEventArgs, ICancelable
    {
        public Player Player { get; set; }

        public bool IsCancel { get; set; }

        public InventoryCloseEventArgs(Inventory inventory, Player player) : base(inventory)
        {
            this.Player = player;
        }
    }
}
