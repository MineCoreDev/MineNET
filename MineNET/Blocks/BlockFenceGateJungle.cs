namespace MineNET.Blocks
{
    public class BlockFenceGateJungle : BlockFenceGateBase
    {
        public BlockFenceGateJungle() : base(BlockFactory.JUNGLE_FENCE_GATE)
        {

        }

        public override string Name
        {
            get
            {
                return "JungleFenceGate";
            }
        }
    }
}
