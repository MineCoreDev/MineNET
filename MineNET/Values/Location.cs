using MineNET.Worlds;

namespace MineNET.Values
{
    public class Location : ILocation
    {
        private float x;
        private float y;
        private float z;

        private float yaw;
        private float pitch;

        private World world;

        public Location(float x, float y, float z, float yaw, float pitch, World world)
        {
            this.x = x;
            this.y = y;
            this.z = z;

            this.yaw = yaw;
            this.pitch = pitch;

            this.world = world;
        }

        public float X
        {
            get
            {
                return x;
            }

            set
            {
                x = value;
            }
        }

        public float Y
        {
            get
            {
                return y;
            }

            set
            {
                y = value;
            }
        }

        public float Z
        {
            get
            {
                return z;
            }

            set
            {
                z = value;
            }
        }

        public float Yaw
        {
            get
            {
                return this.yaw;
            }

            set
            {
                this.yaw = value;
            }
        }

        public float Pitch
        {
            get
            {
                return this.pitch;
            }

            set
            {
                this.pitch = value;
            }
        }

        public World World
        {
            get
            {
                return this.world;
            }

            set
            {
                this.world = value;
            }
        }
    }
}
