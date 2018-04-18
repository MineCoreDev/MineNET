namespace MineNET.Blocks
{
    public class BlockComparatorUnpowered : Block
    {
        public BlockComparatorUnpowered() : base(BlockFactory.UNPOWERED_COMPARATOR)
        {

        }

        public override string Name
        {
            get
            {
                return "UnpoweredComparator";
            }
        }
    }
}
