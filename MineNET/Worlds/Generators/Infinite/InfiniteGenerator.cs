using SharpNoise.Modules;

namespace MineNET.Worlds.Generators.Infinite
{
    public class InfiniteGenerator : GeneratorBase
    {
        public override string Name => "INFINITE";

        private Perlin _perlin;

        public InfiniteGenerator()
        {

        }

        public override void GenerationBasicTerrain(Chunk chunk)
        {
            if (_perlin == null)
            {
                _perlin = new Perlin()
                {
                    Seed = chunk.World.Seed,
                    Frequency = 0.5,
                    Persistence = 0.25
                };
            }

            for (int x = 0; x < 16; x++)
            {
                for (int z = 0; z < 16; z++)
                {
                    int noise = (int) (_perlin.GetValue((double) x / 15d + chunk.X, 0, (double) z / 15d + chunk.Z) * 5) + 10;
                    chunk.SetBlock(x, noise, z, 2);
                    for (int y = noise - 1; y >= 0; y--)
                    {
                        chunk.SetBlock(x, y, z, 3);
                    }
                }
            }
        }

        public override void GenerationOnTheGroundNaturalObjects(Chunk chunk)
        {

        }

        public override void GenerationUnderGroundNaturalObjects(Chunk chunk)
        {

        }
    }
}