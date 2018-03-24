using System;

namespace MineNET.Worlds.Generator
{
    public interface IGenerator : ICloneable
    {
        string Name { get; }
        void ChunkGeneration(Chunk chunk);
    }
}
