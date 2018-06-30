using System;
using MineNET.Worlds;

namespace MineNET.Values
{
    public class Location : ILocation
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public float Yaw { get; set; }
        public float Pitch { get; set; }

        public World World { get; set; }

        public Location(float x, float y, float z, float yaw, float pitch, World world)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;

            this.Yaw = yaw;
            this.Pitch = pitch;

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

        public Vector3 Direction
        {
            get
            {
                return new Vector3(this.Yaw, this.Pitch, this.Yaw);
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object other)
        {
            if (!(other is Location))
            {
                return false;
            }
            Location location = (Location) other;
            return (Vector3) this == (Vector3) location && this.Direction == location.Direction && this.World.Name == location.World.Name;
        }

        public static bool operator ==(Location A, Location B)
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

        public static bool operator !=(Location A, Location B)
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

        public static implicit operator Location(Position p)
        {
            return new Location(p.X, p.Y, p.Z, 0f, 0f, p.World);
        }
    }
}
