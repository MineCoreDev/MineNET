namespace MineNET.Items
{
    public class ItemSplashPotion : Item
    {
        public ItemSplashPotion() : base(ItemFactory.SPLASH_POTION)
        {

        }

        public override string Name
        {
            get
            {
                return "SplashPotion";
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
