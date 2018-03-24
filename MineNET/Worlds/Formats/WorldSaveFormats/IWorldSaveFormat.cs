using MineNET.Worlds.Formats.ChunkFormats;
using MineNET.Worlds.Formats.WorldDataFormats;
using MineNET.Worlds.Generator;

namespace MineNET.Worlds.Formats.WorldSaveFormats
{
    public interface IWorldSaveFormat
    {
        IWorldDataFormat WorldData { get; }
        IChunkFormat ChunkFormat { get; }

        Chunk GetChunk(IGenerator generator, int chunkX, int chunkZ);
        void SetChunk(Chunk chunk);
    }
}
