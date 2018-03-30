namespace MineNET.Items
{
    public class ItemCookedFish : ItemFood
    {
        public ItemCookedFish() : base(ItemFactory.COOKED_FISH)
        {

        }

        public override string Name
        {
            get
            {
                return "CookedFish";
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
