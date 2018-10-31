namespace MineNET.Values
{
    public interface IVector3 : IVector2
    {
        float Z { get; }
    }

    public static class IVector3Extensions
    {
        public static BlockCoordinate3D ToBlockCoordinate3D(this IVector3 vector)
        {
            return new BlockCoordinate3D((int)vector.X, (int)vector.Y, (int) vector.Z);
        }
    }
}
