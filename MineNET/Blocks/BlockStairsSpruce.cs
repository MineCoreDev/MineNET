namespace MineNET.Blocks
{
    public class BlockStairsSpruce : BlockStairsBase
    {
        public BlockStairsSpruce() : base(BlockFactory.SPRUCE_STAIRS)
        {

        }

        public override string Name
        {
            get
            {
                return "SpruceStairs";
            }
        }
    }
}
