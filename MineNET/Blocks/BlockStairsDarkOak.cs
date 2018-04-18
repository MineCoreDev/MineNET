namespace MineNET.Blocks
{
    public class BlockStairsDarkOak : BlockStairsBase
    {
        public BlockStairsDarkOak() : base(BlockFactory.DARK_OAK_STAIRS)
        {

        }

        public override string Name
        {
            get
            {
                return "DarkOakStairs";
            }
        }
    }
}
