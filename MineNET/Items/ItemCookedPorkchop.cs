namespace MineNET.Items
{
    public class ItemCookedPorkchop : ItemFood
    {
        public ItemCookedPorkchop() : base(ItemFactory.COOKED_PORKCHOP)
        {

        }

        public override string Name
        {
            get
            {
                return "CookedPorkchop";
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
