using System;

namespace MineNET.Worlds.Generators
{
    public interface IGenerator : ICloneable
    {
        string Name { get; }
        void ChunkGeneration(Chunk chunk);
    }
}
