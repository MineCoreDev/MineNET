using MineNET.Worlds.Formats.ChunkFormats;
using MineNET.Worlds.Formats.WorldDataFormats;
using System;

namespace MineNET.Worlds.Formats.WorldSaveFormats
{
    public class RegionWorldSaveFormat : IWorldSaveFormat
    {
        public IChunkFormat ChunkFormat => new McaChunkFormat();
        public IWorldDataFormat WorldData => new LevelDBFormat();

        public Chunk GetChunk(int chunkX, int chunkZ)
        {
            throw new NotImplementedException();
        }

        public void SetChunk(Chunk chunk)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}