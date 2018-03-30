namespace MineNET.Items
{
    public class ItemTotem : Item
    {
        public ItemTotem() : base(ItemFactory.TOTEM)
        {

        }

        public override string Name
        {
            get
            {
                return "Totem";
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
