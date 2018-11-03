namespace MineNET.Values
{
    public interface IBlockCoordinate3D : IBlockCoordinate2D
    {
        int Z { get; set; }
    }

    public static class IBlockCoordinate3DExtensions
    {
        public static Vector3 ToVector3(this IBlockCoordinate3D block)
        {
            return new Vector3(block.X, block.Y, block.Z);
        }

        public static BlockCoordinate3D ToBlockCoordinate3D(this IBlockCoordinate3D block)
        {
            return new BlockCoordinate3D(block.X, block.Y, block.Z);
        }
    }
}
