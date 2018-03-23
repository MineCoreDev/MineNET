namespace MineNET.Worlds.Generator
{
    public abstract class GeneratorBase : IGenerator
    {
        public abstract string Name
        {
            get;
        }

        public void ChunkGeneration(Chunk chunk)
        {
            if (!chunk.TerrainPopulated)
            {
                chunk.TerrainPopulated = true;

                this.GenerationBasicTerrain(chunk);
                this.GenerationOnTheGroundNaturalObjects(chunk);
                this.GenerationUnderGroundNaturalObjects(chunk);
                this.GenerationStructures(chunk);
            }
        }

        public abstract void GenerationBasicTerrain(Chunk chunk);
        public abstract void GenerationOnTheGroundNaturalObjects(Chunk chunk);
        public abstract void GenerationUnderGroundNaturalObjects(Chunk chunk);

        public virtual void GenerationStructures(Chunk chunk)
        {

        }
    }
}
