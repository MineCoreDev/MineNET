namespace MineNET.Blocks
{
    public class BlockStairsBrick : BlockStairsBase
    {
        public BlockStairsBrick() : base(BlockFactory.BRICK_STAIRS)
        {

        }

        public override string Name
        {
            get
            {
                return "BrickStairs";
            }
        }
    }
}
