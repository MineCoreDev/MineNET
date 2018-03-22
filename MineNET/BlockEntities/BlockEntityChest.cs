using MineNET.Inventories;
using MineNET.NBT.Data;
using MineNET.NBT.IO;
using MineNET.NBT.Tags;
using MineNET.Values;

namespace MineNET.BlockEntities
{
    public class BlockEntityChest : BlockEntityHolder
    {
        private ChestInventory inventory = null;

        public BlockEntityChest(Position position, CompoundTag nbt = null) : base(position, nbt)
        {
            this.Inventory = new ChestInventory(this);

            if (!this.namedTag.Exist("items"))
            {
                this.namedTag.PutList(new ListTag("items", NBTTagType.COMPOUND));
            }

            ListTag items = this.namedTag.GetList("items");
            for (int i = 0; i < items.Count; ++i)
            {
                this.inventory.SetItem(i, NBTIO.ReadItem((CompoundTag) items[i]));
            }
        }

        public override string Name
        {
            get
            {
                return "Chest";
            }
        }

        public new ChestInventory Inventory
        {
            get
            {
                return (ChestInventory) base.Inventory;
            }

            protected set
            {
                base.Inventory = value;
            }
        }
    }
}
