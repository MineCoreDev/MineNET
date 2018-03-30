namespace MineNET.Items
{
    public class ItemPotion : ItemFood
    {
        public ItemPotion() : base(ItemFactory.POTION)
        {

        }

        public override string Name
        {
            get
            {
                return "Potion";
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
