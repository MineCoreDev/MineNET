namespace MineNET.Blocks
{
    public class BlockComparatorPowered : Block
    {
        public BlockComparatorPowered() : base(BlockFactory.POWERED_COMPARATOR)
        {

        }

        public override string Name
        {
            get
            {
                return "PoweredComparator";
            }
        }
    }
}
