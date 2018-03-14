using System;
using MineNET.Inventories;

namespace MineNET.Events.InventoryEvents
{
    public class InventoryEventArgs : EventArgs
    {
        public Inventory Inventory { get; set; }

        public InventoryEventArgs(Inventory inventory)
        {
            this.Inventory = inventory;
        }
    }
}
