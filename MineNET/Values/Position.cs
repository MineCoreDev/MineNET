using MineNET.Worlds;

namespace MineNET.Values
{
    public class Position : IPosition
    {
        private float x;
        private float y;
        private float z;

        private World world;

        public Position(float x, float y, float z, World world)
        {
            this.x = x;
            this.y = y;
            this.z = z;

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
