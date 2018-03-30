namespace MineNET.Items
{
    public class ItemSnowball : Item
    {
        public ItemSnowball() : base(ItemFactory.SNOWBALL)
        {

        }

        public override string Name
        {
            get
            {
                return "Snowball";
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
