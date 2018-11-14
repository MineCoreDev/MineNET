using MineNET.Values;

namespace MineNET.Blocks
{
    public class BlockSolid : Block
    {
        public BlockSolid(int id, string name) : base(id, name)
        {
            this.IsSolid = true;
            this.BoundingBoxes = new AxisAlignedBB[] { new AxisAlignedBB(Vector3.Zero, Vector3.One) };
        }
    }
}
