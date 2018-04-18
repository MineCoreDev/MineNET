namespace MineNET.Blocks
{
    public class BlockStairsNetherBrick : BlockStairsBase
    {
        public BlockStairsNetherBrick() : base(BlockFactory.NETHER_BRICK_STAIRS)
        {

        }

        public override string Name
        {
            get
            {
                return "NetherBrickStairs";
            }
        }
    }
}
