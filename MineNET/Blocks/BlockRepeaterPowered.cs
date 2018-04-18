namespace MineNET.Blocks
{
    public class BlockRepeaterPowered : Block
    {
        public BlockRepeaterPowered() : base(BlockFactory.POWERED_REPEATER)
        {

        }

        public override string Name
        {
            get
            {
                return "PoweredRepeater";
            }
        }
    }
}
