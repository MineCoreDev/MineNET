namespace MineNET.Blocks
{
    public class BlockStairsStoneBrick : BlockStairsBase
    {
        public BlockStairsStoneBrick() : base(BlockFactory.STONE_BRICK_STAIRS)
        {

        }

        public override string Name
        {
            get
            {
                return "StoneBrickStairs";
            }
        }
    }
}
