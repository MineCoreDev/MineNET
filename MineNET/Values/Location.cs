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
            return this.X.GetHashCode() ^ this.Y.GetHashCode() << 2 ^ this.Z.GetHashCode() >> 2 ^ this.World.GetHashCode() ^ this.Yaw.GetHashCode() << 2 ^ this.Pitch.GetHashCode() >> 2;
        }

        public override string ToString()
        {
            return $"Location: World = {this.World.Name}, X = {this.X}, Y = {this.Y}, Z = {this.Z}, Yaw = {this.Yaw}, Pitch = {this.Pitch}";
        }

        public Vector3 GetVector3()
        {
            return new Vector3(this.X, this.Y, this.Z);
        }

        public Vector2 GetVector2()
        {
            return new Vector2(this.X, this.Z);
        }

        public Vector2 GetRotateVector2()
        {
            return new Vector2(this.Yaw, this.Pitch);
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
