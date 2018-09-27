using MineNET.BlockEntities;
using MineNET.Inventories.Data;

namespace MineNET.Inventories
{
    public class ChestInventory : ContainerInventory
    {
        public ChestInventory(BlockEntityChest holder) : base(holder)
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
                return InventoryType.CONTAINER.GetIndex();
            }
        }

        public override string Name
        {
            get
            {
                return "Chest";
            }
        }

        public new BlockEntityChest Holder
        {
            get
            {
                return (BlockEntityChest) base.Holder;
            }

            set
            {
                base.Holder = value;
            }
        }
    }
}
