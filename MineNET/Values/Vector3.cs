using System;
using MineNET.Worlds;

namespace MineNET.Values
{
    public struct Vector3 : IVector3
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Vector3(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

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

        public int FloorX
        {
            get
            {
                return (int) Math.Floor(this.X);
            }
        }

        public int FloorY
        {
            get
            {
                return (int) Math.Floor(this.Y);
            }
        }

        public int FloorZ
        {
            get
            {
                return (int) Math.Floor(this.Z);
            }
        }

        public float Magnitude
        {
            get
            {
                return (float) Math.Sqrt(this.X * this.X + this.Y * this.Y + this.Z * this.Z);
            }
        }

        public float SqrtMagnitude
        {
            get
            {
                return this.X * this.X + this.Y * this.Y + this.Z * this.Z;
            }
        }

        public static float Distance(Vector3 a, Vector3 b)
        {
            return (a - b).Magnitude;
        }

        public static float DistanceSquared(Vector3 a, Vector3 b)
        {
            return (a - b).SqrtMagnitude;
        }

        public override string ToString()
        {
            return $"X: {this.X} Y: {this.Y} Z: {this.Z}";
        }

        public Position Position(World world)
        {
            return new Position(this.X, this.Y, this.Z, world);
        }

        public static Vector3 operator +(Vector3 A, Vector3 B)
        {
            return new Vector3(A.X + B.X, A.Y + B.Y, A.Z + B.Z);
        }

        public static Vector3 operator -(Vector3 A, Vector3 B)
        {
            return new Vector3(A.X - B.X, A.Y - B.Y, A.Z - B.Z);
        }

        public static Position operator +(Vector3 p, World world)
        {
            return new Position(p.X, p.Y, p.X, world);
        }

        public static implicit operator Vector3(Vector2 v)
        {
            return new Vector3(v.X, v.Y, 0f);
        }

        public static implicit operator Vector3(Vector3i v)
        {
            return new Vector3(v.X, v.Y, v.Z);
        }

        public static explicit operator Vector3(Position p)
        {
            return new Vector3(p.X, p.Y, p.Z);
        }

        public static explicit operator Vector3(Location l)
        {
            return new Vector3(l.X, l.Y, l.Z);
        }
    }
}
