using System;
using MineNET.Worlds.Formats.ChunkFormats;
using MineNET.Worlds.Formats.WorldDataFormats;

namespace MineNET.Worlds.Formats.WorldSaveFormats
{
    public class MineNETWorldSaveFormat : IWorldSaveFormat
    {
        public IChunkFormat ChunkFormat
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IWorldDataFormat WorldData
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Chunk GetChunk(int chunkX, int chunkZ)
        {
            throw new NotImplementedException();
        }

        public void SetChunk(Chunk chunk)
        {
            throw new NotImplementedException();
        }
    }
}
