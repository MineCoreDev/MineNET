using MineNET.Worlds;

namespace MineNET.Values
{
    public struct Vector3i : IVector3i
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public Vector3i(int x, int y, int z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public static Vector3i operator +(Vector3i A, Vector3i B)
        {
            return new Vector3i(A.X + B.X, A.Y + B.Y, A.Z + B.Z);
        }

        public static Vector3i operator -(Vector3i A, Vector3i B)
        {
            return new Vector3i(A.X - B.X, A.Y - B.Y, A.Z - B.Z);
        }

        public static Position operator +(Vector3i v, World world)
        {
            return new Position(v.X, v.Y, v.Z, world);
        }

        public static implicit operator Vector3i(Vector3 v)
        {
            return new Vector3i(v.FloorX, v.FloorY, v.FloorZ);
        }

        public static implicit operator Vector3i(Vector2 v)
        {
            return new Vector3i(v.FloorX, v.FloorY, 0);
        }

        public static explicit operator Vector3i(Position p)
        {
            return new Vector3i(p.FloorX, p.FloorY, p.FloorZ);
        }

        public static explicit operator Vector3i(Location l)
        {
            return new Vector3i(l.FloorX, l.FloorY, l.FloorZ);
        }
    }
}
