namespace MineNET.Items
{
    public class ItemAppleenchanted : ItemFood
    {
        public ItemAppleenchanted() : base(ItemFactory.APPLEENCHANTED)
        {

        }

        public override string Name
        {
            get
            {
                return "Appleenchanted";
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
                return 9.6f;
            }
        }
    }
}
