namespace MineNET.Worlds.Generator
{
    public interface IGenerator
    {
        string Name { get; }
        void ChunkGeneration(Chunk chunk);
    }
}
