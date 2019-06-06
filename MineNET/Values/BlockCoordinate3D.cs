using System;

namespace MineNET.Values
{
    public struct BlockCoordinate3D : IBlockCoordinate3D
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public BlockCoordinate3D(int x, int y, int z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public BlockCoordinate3D(float x, float y, float z)
        {
            this.X = (int) Math.Floor(x);
            this.Y = (int) Math.Floor(y);
            this.Z = (int) Math.Floor(z);
        }

        public static explicit operator BlockCoordinate3D(Vector3 v)
        {
            return new BlockCoordinate3D(v.FloorX, v.FloorY, v.FloorZ);
        }

        public override string ToString()
        {
            return $"BlockCoordinate3D: X = {this.X}, Y = {this.Y}, Z = {this.Z}";
        }
    }
}