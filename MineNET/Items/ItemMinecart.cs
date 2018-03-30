namespace MineNET.Items
{
    public class ItemMinecart : Item
    {
        public ItemMinecart() : base(ItemFactory.MINECART)
        {

        }

        public override string Name
        {
            get
            {
                return "Minecart";
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
