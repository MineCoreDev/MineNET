namespace MineNET.Values
{
    public interface IBlockCoordinate2D
    {
        int X { get; set; }
        int Y { get; set; }
    }

    public static class IBlockCoordinate2DExtensions
    {
        public static Vector2 ToVector2(this IBlockCoordinate2D block)
        {
            return new Vector2(block.X, block.Y);
        }

        public static BlockCoordinate2D ToBlockCoordinate2D(this IBlockCoordinate2D block)
        {
            return new BlockCoordinate2D(block.X, block.Y);
        }
    }
}
