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

        public float this[int index]
        {
            get
            {
                if (index == 0)
                {
                    return this.X;
                }
                if (index == 1)
                {
                    return this.Y;
                }
                if (index != 2)
                {
                    throw new IndexOutOfRangeException();
                }
                return this.Y;
            }

            set
            {
                if (index != 0)
                {
                    if (index == 1)
                    {
                        this.Y = value;
                    }
                    if (index != 2)
                    {
                        throw new IndexOutOfRangeException();
                    }
                    this.Y = value;
                }
                else
                {
                    this.X = value;
                }
            }
        }

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

        public override string ToString()
        {
            return $"X: {this.X} Y: {this.Y} Z: {this.Z}";
        }

        public Vector3i ToVector3i()
        {
            return new Vector3i(this.GetFloorX(), this.GetFloorY(), this.GetFloorZ());
        }
    }
}
