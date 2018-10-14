using MineNET.NBT.Tags;
using MineNET.Values;
using MineNET.Worlds;

namespace MineNET.BlockEntities
{
    public abstract class BlockEntitySpawnable : BlockEntity
    {
        public BlockEntitySpawnable(Chunk chunk, CompoundTag nbt) : base(chunk, nbt)
        {

        }
    }
}
