namespace MineNET.Items
{
    public class ItemBeetrootSoup : ItemFood
    {
        public ItemBeetrootSoup() : base(ItemFactory.BEETROOT_SOUP)
        {

        }

        public override string Name
        {
            get
            {
                return "BeetrootSoup";
            }
        }

        public override int FoodRestore
        {
            get
            {
                return 6;
            }
        }

        public override float SaturationRestore
        {
            get
            {
                return 7.2f;
            }
        }
    }
}
