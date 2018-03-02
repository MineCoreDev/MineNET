using MineNET.Worlds;

namespace MineNET.Values
{
    public class Location : ILocation
    {
        public Location(float x, float y, float z, float yaw, float pitch, World world)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;

            this.Yaw = yaw;
            this.Pitch = pitch;

            this.World = world;
        }

        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public float Yaw { get; set; }
        public float Pitch { get; set; }

        public World World { get; set; }
    }
}
