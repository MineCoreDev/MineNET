namespace MineNET.Items
{
    public class ItemWoodenHoe : ItemTool
    {
        public ItemWoodenHoe() : base(ItemFactory.WOODEN_HOE)
        {

        }

        public override string Name
        {
            get
            {
                return "WoodenHoe";
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
                return 60;
            }
        }
    }
}
