namespace MineNET.Blocks
{
    public class BlockFenceNetherBrick : Block
    {
        public BlockFenceNetherBrick() : base(BlockFactory.NETHER_BRICK_FENCE)
        {

        }

        public override string Name
        {
            get
            {
                return "NetherBrickFence";
            }
        }
    }
}
