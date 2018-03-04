using System;

namespace MineNET.Values
{
    public struct Vector2
    {

        public Vector2(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        public float X { get; set; }
        public float Y { get; set; }

        public float this[int index]
        {
            get
            {
                if (index == 0)
                {
                    return this.X;
                }
                if (index != 1)
                {
                    throw new IndexOutOfRangeException();
                }
                return this.Y;
            }

            set
            {
                if (index != 0)
                {
                    if (index != 1)
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

        public override string ToString()
        {
            return $"X: {this.X} Y: {this.Y}";
        }
    }
}
