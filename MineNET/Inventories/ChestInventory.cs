using System.Collections.Generic;
using MineNET.Inventories.Data;
using MineNET.Items;

namespace MineNET.Inventories
{
    public class ChestInventory : ContainerInventory
    {
        public ChestInventory(InventoryHolder holder, Dictionary<int, Item> items = null) : base(holder, items)
        {

        }

        public override int Size
        {
            get
            {
                return 27;
            }
        }

        public override byte Type
        {
            get
            {
                return (byte) InventoryType.CONTAINER;
            }
        }
    }
}
