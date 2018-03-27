using MineNET.BlockEntities;
using MineNET.Inventories.Data;
using MineNET.Items;
using MineNET.NBT.Data;
using MineNET.NBT.IO;
using MineNET.NBT.Tags;

namespace MineNET.Inventories
{
    public class ChestInventory : ContainerInventory
    {
        public ChestInventory(BlockEntityChest holder) : base(holder)
        {
            if (!holder.NamedTag.Exist("Items"))
            {
                ListTag initItems = new ListTag("Items", NBTTagType.COMPOUND);
                for (int i = 0; i < this.Size; ++i)
                {
                    initItems.Add(NBTIO.WriteItem(Item.Get(0, 0, 0), i));
                }
                holder.NamedTag.PutList(initItems);
            }

            ListTag items = holder.NamedTag.GetList("Items");
            for (int i = 0; i < this.Size; ++i)
            {
                Item item = NBTIO.ReadItem((CompoundTag) items[i]);
                this.SetItem(i, item, false);
            }
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

        public override void SaveNBT()
        {
            ListTag inventory = new ListTag("Items", NBTTagType.COMPOUND);
            for (int i = 0; i < this.Size; ++i)
            {
                inventory.Add(NBTIO.WriteItem(this.GetItem(i), i));
            }
            this.Holder.NamedTag.PutList(inventory);
        }
    }
}
