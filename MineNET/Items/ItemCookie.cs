namespace MineNET.Items
{
    public class ItemCookie : ItemFood
    {
        public ItemCookie() : base(ItemFactory.COOKIE)
        {

        }

        public override string Name
        {
            get
            {
                return "Cookie";
            }
        }

        public override int FoodRestore
        {
            get
            {
                return 2;
            }
        }

        public override float SaturationRestore
        {
            get
            {
                return 0.4f;
            }
        }
    }
}
