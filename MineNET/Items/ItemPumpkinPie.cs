namespace MineNET.Items
{
    public class ItemPumpkinPie : ItemFood
    {
        public ItemPumpkinPie() : base(ItemFactory.PUMPKIN_PIE)
        {

        }

        public override string Name
        {
            get
            {
                return "PumpkinPie";
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
                return 4.8f;
            }
        }
    }
}
