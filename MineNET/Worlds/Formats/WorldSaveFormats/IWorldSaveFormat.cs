using System;
using System.Collections.Generic;
using MineNET.Worlds.Formats.ChunkFormats;
using MineNET.Worlds.Formats.WorldDataFormats;

namespace MineNET.Worlds.Formats.WorldSaveFormats
{
    public interface IWorldSaveFormat
    {
        IWorldDataFormat WorldData { get; }
        IChunkFormat ChunkFormat { get; }
        World World { get; }

        void SetWorld(World world);
        Chunk GetChunk(int chunkX, int chunkZ);
        void SetChunk(Chunk chunk);
        void Save(Dictionary<Tuple<int, int>, Chunk> chunks);
    }
}
