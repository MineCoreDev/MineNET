using MineNET.Worlds.Dimensions;
using MineNET.Worlds.Generators;

namespace MineNET.Worlds
{
    public class World
    {
        #region Property & Field
        public string Name { get; set; } = "World";
        public int Seed { get; private set; } = -1;
        public byte Dimension { get; private set; } = DimensionID.OverWorld;
        public int Generator { get; private set; } = GeneratorID.Infinite;
        public GameMode Gamemode { get; set; } = GameMode.Survival;
        public Difficulty Difficulty { get; set; } = Difficulty.Normal;

        public int SpawnX { get; set; } = 128;
        public int SpawnY { get; set; } = 5;
        public int SpawnZ { get; set; } = 128;
        #endregion
    }
}
