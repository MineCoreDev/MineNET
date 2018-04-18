namespace MineNET.Blocks
{
    public class BlockStairsBirch : BlockStairsBase
    {
        public BlockStairsBirch() : base(BlockFactory.BIRCH_STAIRS)
        {

        }

        public override string Name
        {
            get
            {
                return "BirchStairs";
            }
        }
    }
}
