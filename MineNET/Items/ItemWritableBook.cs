namespace MineNET.Items
{
    public class ItemWritableBook : Item
    {
        public ItemWritableBook() : base(ItemFactory.WRITABLE_BOOK)
        {

        }

        public override string Name
        {
            get
            {
                return "WritableBook";
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
