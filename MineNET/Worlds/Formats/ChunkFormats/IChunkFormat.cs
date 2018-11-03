using MineNET.NBT.Tags;

namespace MineNET.Worlds.Formats.ChunkFormats
{
    public interface IChunkFormat
    {
        World World { get; }

        CompoundTag NBTSerialize(Chunk chunk);
        Chunk NBTDeserialize(CompoundTag tag);
    }
}
