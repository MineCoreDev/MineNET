namespace MineNET.Items
{
    public class ItemMelon : ItemFood
    {
        public ItemMelon() : base(ItemFactory.MELON)
        {

        }

        public override string Name
        {
            get
            {
                return "Melon";
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
                return 1.2f;
            }
        }
    }
}
