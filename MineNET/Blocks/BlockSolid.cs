using MineNET.Values;

namespace MineNET.Blocks
{
    public abstract class BlockSolid : Block
    {
        public BlockSolid(string name, int id) : base(name, id)
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
