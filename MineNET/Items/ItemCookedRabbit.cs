namespace MineNET.Items
{
    public class ItemCookedRabbit : ItemFood
    {
        public ItemCookedRabbit() : base(ItemFactory.COOKED_RABBIT)
        {

        }

        public override string Name
        {
            get
            {
                return "CookedRabbit";
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
