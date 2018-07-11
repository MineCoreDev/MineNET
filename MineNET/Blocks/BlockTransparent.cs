namespace MineNET.Blocks
{
    public abstract class BlockTransparent : Block
    {
        public BlockTransparent(int id, string name) : base(id, name)
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
