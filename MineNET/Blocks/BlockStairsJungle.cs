namespace MineNET.Blocks
{
    public class BlockStairsJungle : BlockStairsBase
    {
        public BlockStairsJungle() : base(BlockFactory.JUNGLE_STAIRS)
        {

        }

        public override string Name
        {
            get
            {
                return "JungleStairs";
            }
        }
    }
}
