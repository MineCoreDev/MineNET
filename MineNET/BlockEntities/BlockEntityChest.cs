using MineNET.Inventories;
using MineNET.NBT.Data;
using MineNET.NBT.IO;
using MineNET.NBT.Tags;
using MineNET.Values;
using MineNET.Worlds;

namespace MineNET.BlockEntities
{
    /// <summary>
    /// Minecraft に存在するチェストの <see cref="BlockEntity"/> です。
    /// </summary>
    public class BlockEntityChest : BlockEntity, InventoryHolder
    {
        Inventory InventoryHolder.Inventory
        {
            get
            {
                return this.Inventory;
            }
        }

        public ChestInventory Inventory { get; protected set; }

        /// <summary>
        /// <see cref="BlockEntityChest"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="chunk"></param>
        /// <param name="nbt"></param>
        public BlockEntityChest(Chunk chunk, CompoundTag nbt = null) : base(chunk, nbt)
        {
            this.Inventory = new ChestInventory(this);

            if (!this.NamedTag.Exist("items"))
            {
                this.NamedTag.PutList(new ListTag("items", NBTTagType.COMPOUND));
            }

            ListTag items = this.NamedTag.GetList("items");
            for (int i = 0; i < items.Count; ++i)
            {
                this.Inventory.SetItem(i, NBTIO.ReadItem((CompoundTag) items[i]));
            }
        }

        /// <summary>
        /// <see cref="BlockEntityChest"/> の名前
        /// </summary>
        public override string Name
        {
            get
            {
                return "Chest";
            }
        }
    }
}
