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
