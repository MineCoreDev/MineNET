using MineNET.Inventories;
using MineNET.NBT.Tags;
using MineNET.Values;

namespace MineNET.BlockEntities
{
    public abstract class BlockEntityHolder : BlockEntitySpawnable, InventoryHolder
    {
        public Inventory Inventory { get; protected set; }

        public BlockEntityHolder(Position position, CompoundTag nbt) : base(position, nbt)
        {

        }
    }
}
