namespace MineNET.Items
{
    public class ItemWoodenSword : ItemTool
    {
        public ItemWoodenSword() : base(ItemFactory.WOODEN_SWORD)
        {

        }

        public override string Name
        {
            get
            {
                return "WoodenSword";
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
                return 60;
            }
        }
    }
}
