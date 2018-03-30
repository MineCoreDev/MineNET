namespace MineNET.Items
{
    public class ItemApple : ItemFood
    {
        public ItemApple() : base(ItemFactory.APPLE)
        {

        }

        public override string Name
        {
            get
            {
                return "Apple";
            }
        }

        public override int FoodRestore
        {
            get
            {
                return 4;
            }
        }

        public override float SaturationRestore
        {
            get
            {
                return 2.4f;
            }
        }
    }
}
