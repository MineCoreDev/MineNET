namespace MineNET.Items
{
    public class ItemFish : ItemFood
    {
        public ItemFish() : base(ItemFactory.FISH)
        {

        }

        public override string Name
        {
            get
            {
                return "Fish";
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
