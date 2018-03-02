using MineNET.NBT.Tags;
using MineNET.Values;

namespace MineNET.BlockEntities
{
    public abstract class BlockEntitySpawnable : BlockEntity
    {
        public BlockEntitySpawnable(Position position, CompoundTag nbt = null) : base(position, nbt)
        {

        }
    }
}
