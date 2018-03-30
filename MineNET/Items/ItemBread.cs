namespace MineNET.Items
{
    public class ItemBread : ItemFood
    {
        public ItemBread() : base(ItemFactory.BREAD)
        {

        }

        public override string Name
        {
            get
            {
                return "Bread";
            }
        }

        public override int FoodRestore
        {
            get
            {
                return 5;
            }
        }

        public override float SaturationRestore
        {
            get
            {
                return 6f;
            }
        }
    }
}
