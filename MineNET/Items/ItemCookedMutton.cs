namespace MineNET.Items
{
    public class ItemCookedMutton : ItemFood
    {
        public ItemCookedMutton() : base(ItemFactory.COOKED_MUTTON)
        {

        }

        public override string Name
        {
            get
            {
                return "CookedMutton";
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
