namespace MineNET.Items
{
    public class ItemDiamondSword : ItemTool
    {
        public ItemDiamondSword() : base(ItemFactory.DIAMOND_SWORD)
        {

        }

        public override string Name
        {
            get
            {
                return "DiamondSword";
            }
        }

        public override bool IsSword
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
