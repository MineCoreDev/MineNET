namespace MineNET.Blocks
{
    public abstract class BlockTransparent : Block
    {
        public BlockTransparent(int id) : base(id)
        {

        }

        public override bool IsTransparent
        {
            get
            {
                return true;
            }
        }
    }
}
