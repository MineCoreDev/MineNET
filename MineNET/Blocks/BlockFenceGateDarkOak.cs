namespace MineNET.Blocks
{
    public class BlockFenceGateDarkOak : BlockFenceGateBase
    {
        public BlockFenceGateDarkOak() : base(BlockFactory.DARK_OAK_FENCE_GATE)
        {

        }

        public override string Name
        {
            get
            {
                return "DarkOakFenceGate";
            }
        }
    }
}
