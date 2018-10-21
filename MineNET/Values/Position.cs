using System;
using MineNET.Worlds;

namespace MineNET.Values
{
    public class Position : IPosition
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public World World { get; set; }

        public Position(float x, float y, float z, World world)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;

            this.World = world;
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

        public Vector3 GetVector3()
        {
            return new Vector3(this.X, this.Y, this.Z);
        }

        public Vector2 GetVector2()
        {
            return new Vector2(this.X, this.Z);
        }

        public override int GetHashCode()
        {
            return this.X.GetHashCode() ^ this.Y.GetHashCode() << 2 ^ this.Z.GetHashCode() >> 2 ^ this.World.GetHashCode();
        }

        public override string ToString()
        {
            return $"Position: World = {this.World.Name}, X = {this.X}, Y = {this.Y}, Z = {this.Z}";
        }

        public override bool Equals(object other)
        {
            if (!(other is Position))
            {
                return false;
            }
            Position position = (Position) other;
            return (Vector3) this == (Vector3) position && this.World.Name == position.World.Name;
        }

        public static bool operator ==(Position A, Position B)
        {
            if (object.ReferenceEquals(A, B))
            {
                return true;
            }
            if ((object) A == null || (object) B == null)
            {
                return false;
            }
            return A.Equals(B);
        }

        public static bool operator !=(Position A, Position B)
        {
            if (object.ReferenceEquals(A, B))
            {
                return false;
            }
            if ((object) A == null || (object) B == null)
            {
                return true;
            }
            return !A.Equals(B);
        }

        public static Location operator +(Position p, Vector2 v)
        {
            return new Location(p.X, p.Y, p.Z, v.X, v.Y, p.World);
        }

        public static explicit operator Position(Location l)
        {
            return new Position(l.X, l.Y, l.Z, l.World);
        }
    }
}
