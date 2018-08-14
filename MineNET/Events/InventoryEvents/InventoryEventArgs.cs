using System;
using MineNET.Inventories;

namespace MineNET.Events.InventoryEvents
{
    public abstract class InventoryEventArgs : EventArgs
    {
        public Inventory Inventory { get; }

        public InventoryEventArgs(Inventory inventory)
        {
            this.Inventory = inventory;
        }
    }
}
