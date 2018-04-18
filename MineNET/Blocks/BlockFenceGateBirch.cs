namespace MineNET.Blocks
{
    public class BlockFenceGateBirch : BlockFenceGateBase
    {
        public BlockFenceGateBirch() : base(BlockFactory.BIRCH_FENCE_GATE)
        {

        }

        public override string Name
        {
            get
            {
                return "BirchFenceGate";
            }
        }
    }
}
