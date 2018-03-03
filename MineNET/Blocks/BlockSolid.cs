namespace MineNET.Blocks
{
    public abstract class BlockSolid : Block
    {
        public BlockSolid(int id) : base(id)
        {

        }

        public override bool IsSolid
        {
            get
            {
                return true;
            }
        }
    }
}
