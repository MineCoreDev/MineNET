namespace MineNET.Items
{
    public class ItemCookedBeef : ItemFood
    {
        public ItemCookedBeef() : base(ItemFactory.COOKED_BEEF)
        {

        }

        public override string Name
        {
            get
            {
                return "CookedBeef";
            }
        }

        public override int FoodRestore
        {
            get
            {
                return 8;
            }
        }

        public override float SaturationRestore
        {
            get
            {
                return 12.8f;
            }
        }
    }
}
