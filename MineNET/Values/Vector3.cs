using System;

namespace MineNET.Values
{
    public struct Vector3 : IVector3
    {
        public Vector3(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public int GetFloorX()
        {
            return (int) Math.Floor(this.X);
        }

        public int GetFloorY()
        {
            return (int) Math.Floor(this.Y);
        }

        public int GetFloorZ()
        {
            return (int) Math.Floor(this.Z);
        }
    }
}
