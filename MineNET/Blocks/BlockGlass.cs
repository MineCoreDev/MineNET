namespace MineNET.Blocks
{
    public class BlockGlass : BlockTransparent
    {
        public BlockGlass() : base(BlockFactory.GLASS)
        {

        }

        public override string Name
        {
            get
            {
                return "Glass";
            }
        }
    }
}
