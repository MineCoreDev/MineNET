namespace MineNET.Items
{
    public class ItemDiamondHoe : ItemTool
    {
        public ItemDiamondHoe() : base(ItemFactory.DIAMOND_HOE)
        {

        }

        public override string Name
        {
            get
            {
                return "DiamondHoe";
            }
        }

        public override bool IsHoe
        {
            get
            {
                return true;
            }
        }

        public override int MaxDurability
        {
            get
            {
                return 1562;
            }
        }
    }
}
