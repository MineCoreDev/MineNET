using System;

namespace MineNET.Values
{
    public struct Vector3 : IVector3
    {
        private float x;
        private float y;
        private float z;

        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
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

        public int GetFloorX()
        {
            return (int) Math.Floor(this.x);
        }

        public int GetFloorY()
        {
            return (int) Math.Floor(this.y);
        }

        public int GetFloorZ()
        {
            return (int) Math.Floor(this.z);
        }
    }
}
