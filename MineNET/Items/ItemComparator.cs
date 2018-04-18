using MineNET.Blocks;

namespace MineNET.Items
{
    public class ItemComparator : Item
    {
        public ItemComparator() : base(ItemFactory.COMPARATOR)
        {
            this.Block = new BlockComparatorUnpowered();
        }

        public override string Name
        {
            get
            {
                return "Comparator";
            }
        }
    }
}
