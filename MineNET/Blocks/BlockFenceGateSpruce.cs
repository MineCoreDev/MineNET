namespace MineNET.Blocks
{
    public class BlockFenceGateSpruce : BlockFenceGateBase
    {
        public BlockFenceGateSpruce() : base(BlockFactory.SPRUCE_FENCE_GATE)
        {

        }

        public override string Name
        {
            get
            {
                return "SpruceFenceGate";
            }
        }
    }
}
