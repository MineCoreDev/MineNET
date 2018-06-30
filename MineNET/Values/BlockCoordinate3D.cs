namespace MineNET.Values
{
    public struct BlockCoordinate3D : IVector3i
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
    }
}
