namespace MineNET.Blocks
{
    public class BlockFenceGateAcacia : BlockFenceGateBase
    {
        public BlockFenceGateAcacia() : base(BlockFactory.ACACIA_FENCE_GATE)
        {

        }

        public override string Name
        {
            get
            {
                return "AcaciaFenceGate";
            }
        }
    }
}
