using MineNET.Worlds;

namespace MineNET.Values
{
    public class Position : IPosition
    {
        public Position(float x, float y, float z, World world)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;

            this.World = world;
        }

        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public World World { get; set; }
    }
}
