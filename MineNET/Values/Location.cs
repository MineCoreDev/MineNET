using MineNET.Worlds;

namespace MineNET.Values
{
    public class Location : ILocation
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public float Yaw { get; set; }
        public float Pitch { get; set; }

        public World World { get; set; }

        public Location(float x, float y, float z, float yaw, float pitch, World world)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;

            this.Yaw = yaw;
            this.Pitch = pitch;

            this.World = world;
        }

        public Position Position
        {
            get
            {
                return new Position(this.X, this.Y, this.Z, this.World);
            }
        }

        public Vector3 Vector3
        {
            get
            {
                return new Vector3(this.X, this.Y, this.Z);
            }
        }

        public Vector2 Vector2
        {
            get
            {
                return new Vector2(this.X, this.Y);
            }
        }
    }
}
