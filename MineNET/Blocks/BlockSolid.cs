using MineNET.Values;

namespace MineNET.Blocks
{
    public class BlockSolid : Block
    {
        public BlockSolid(int id, string name) : base(id, name)
        {
        }

        public override bool IsSolid
        {
            get
            {
                return true;
            }
        }

        public override AxisAlignedBB BoundingBox
        {
            get
            {
                return new AxisAlignedBB(Vector3.One, Vector3.One);
            }
        }
    }
}
