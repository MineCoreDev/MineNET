namespace MineNET.Values
{
    public interface IBlockCoordinate3D : IBlockCoordinate2D
    {
        int Z { get; set; }
    }

    public static class BlockCoordinate3DExtensions
    {
        public static Vector3 ToVector3(this BlockCoordinate3D block)
        {
            return new Vector3(block.X, block.Y, block.Z);
        }
    }
}
