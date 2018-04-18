namespace MineNET.Blocks
{
    public class BlockRepeaterUnpowered : Block
    {
        public BlockRepeaterUnpowered() : base(BlockFactory.UNPOWERED_REPEATER)
        {

        }

        public override string Name
        {
            get
            {
                return "UnpoweredRepeater";
            }
        }
    }
}
