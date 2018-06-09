namespace MineNET.Items
{
    public class ItemDiamondShovel : ItemTool
    {
        public ItemDiamondShovel() : base(ItemFactory.DIAMOND_SHOVEL)
        {

        }

        public override string Name
        {
            get
            {
                return "DiamondShovel";
            }
        }

        public override bool IsShovel
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
