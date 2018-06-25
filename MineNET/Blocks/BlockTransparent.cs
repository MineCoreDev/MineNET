namespace MineNET.Blocks
{
    public abstract class BlockTransparent : Block
    {
        public BlockTransparent(string name, int id) : base(name, id)
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
