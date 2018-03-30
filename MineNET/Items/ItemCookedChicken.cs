namespace MineNET.Items
{
    public class ItemCookedChicken : ItemFood
    {
        public ItemCookedChicken() : base(ItemFactory.COOKED_CHICKEN)
        {

        }

        public override string Name
        {
            get
            {
                return "CookedChicken";
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
