namespace MineNET.Items
{
    public class ItemEnchantedBook : Item
    {
        public ItemEnchantedBook() : base(ItemFactory.ENCHANTED_BOOK)
        {

        }

        public override string Name
        {
            get
            {
                return "EnchantedBook";
            }
        }

        public override byte MaxStackSize
        {
            get
            {
                return 1;
            }
        }
    }
}
