namespace MineNET.Items
{
    public class ItemBanner : Item
    {
        public ItemBanner() : base(ItemFactory.BANNER)
        {

        }

        public override string Name
        {
            get
            {
                return "Banner";
            }
        }

        public override byte MaxStackSize
        {
            get
            {
                return 16;
            }
        }
    }
}
