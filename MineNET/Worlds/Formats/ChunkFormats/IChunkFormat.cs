using MineNET.NBT.Tags;

namespace MineNET.Worlds.Formats.ChunkFormats
{
    public interface IChunkFormat
    {
        CompoundTag NBTSerialize(Chunk chunk);
        Chunk NBTDeserialize(CompoundTag tag);
    }
}
