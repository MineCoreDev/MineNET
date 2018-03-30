namespace MineNET.Items
{
    public class ItemCookedSalmon : ItemFood
    {
        public ItemCookedSalmon() : base(ItemFactory.COOKED_SALMON)
        {

        }

        public override string Name
        {
            get
            {
                return "CookedSalmon";
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
                return 9.6f;
            }
        }
    }
}
