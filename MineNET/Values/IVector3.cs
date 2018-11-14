using System;

namespace MineNET.Values
{
    public interface IVector3 : IVector2
    {
        float Z { get; set; }
    }

    public static class IVector3Extensions
    {
        public static Vector3 ToVector3(this IVector3 vector)
        {
            return new Vector3(vector.X, vector.Y, vector.Z);
        }

        public static BlockCoordinate3D ToBlockCoordinate3D(this IVector3 vector)
        {
            return new BlockCoordinate3D((int)vector.X, (int)vector.Y, (int) vector.Z);
        }


        public static int GetFloorX(this IVector3 vector)
        {
            return (int) Math.Floor(vector.X);
        }

        public static int GetFloorY(this IVector3 vector)
        {
            return (int) Math.Floor(vector.Y);
        }

        public static int GetFloorZ(this IVector3 vector)
        {
            return (int) Math.Floor(vector.Z);
        }

        public static float Distance(this IVector3 vector, IVector3 pos)
        {
            return (float) Math.Sqrt(vector.DistanceSquared(pos));
        }

        public static float DistanceSquared(this IVector3 vector, IVector3 pos)
        {
            float x = vector.X - pos.X;
            float y = vector.Y - pos.Y;
            float z = vector.Z - pos.Z;
            return (x * x) + (y * y) + (z * z);
        }

        public static float Length(this IVector3 vector)
        {
            return (float) Math.Sqrt(vector.LengthSquared());
        }

        public static float LengthSquared(this IVector3 vector)
        {
            return (vector.X * vector.X) + (vector.Y * vector.Y) + (vector.Z * vector.Z);
        }
    }
}
