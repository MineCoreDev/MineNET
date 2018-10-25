namespace MineNET.Values
{
    public struct BlockCoordinate2D : IBlockCoordinate2D
    {
        public int X { get; set; }
        public int Y { get; set; }

        public BlockCoordinate2D(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
