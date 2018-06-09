namespace MineNET.Items
{
    public class ItemWoodenShovel : ItemTool
    {
        public ItemWoodenShovel() : base(ItemFactory.WOODEN_SHOVEL)
        {

        }

        public override string Name
        {
            get
            {
                return "WoodenShovel";
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
                return 60;
            }
        }
    }
}
