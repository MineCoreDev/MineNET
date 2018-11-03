namespace MineNET.Values
{
    public interface IVector2
    {
        float X { get; set; }
        float Y { get; set; }
    }

    public static class IVector2Extensions
    {
        public static Vector2 ToVector2(this IVector2 vector)
        {
            return new Vector2(vector.X, vector.Y);
        }

        public static BlockCoordinate2D ToBlockCoordinate2D(this IVector2 vector)
        {
            return new BlockCoordinate2D((int)vector.X, (int)vector.Y);
        }
    }
}
