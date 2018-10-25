using MineNET.Inventories;
using MineNET.NBT.Tags;
using MineNET.Worlds;

namespace MineNET.BlockEntities
{
    /// <summary>
    /// Minecraft に存在するチェストの <see cref="BlockEntity"/> です。
    /// </summary>
    public class BlockEntityChest : BlockEntity, InventoryHolder
    {
        public ChestInventory Inventory { get; protected set; }

        /// <summary>
        /// <see cref="BlockEntityChest"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="chunk"></param>
        /// <param name="nbt"></param>
        public BlockEntityChest(Chunk chunk, CompoundTag nbt = null) : base(chunk, nbt)
        {
            this.Inventory = new ChestInventory(this);
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

        public override CompoundTag SaveNBT()
        {
            CompoundTag nbt = base.SaveNBT();
            CompoundTag items = this.Inventory.SaveNBT();
            foreach (string name in items.Tags.Keys)
            {
                Tag tag = items.GetTag(name);
                nbt.PutTag(name, tag);
            }
            return nbt;
        }

        Inventory InventoryHolder.Inventory
        {
            get
            {
                return this.Inventory;
            }
        }

    }
}
