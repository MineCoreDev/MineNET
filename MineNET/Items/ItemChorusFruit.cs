namespace MineNET.Items
{
    public class ItemChorusFruit : ItemFood
    {
        public ItemChorusFruit() : base(ItemFactory.CHORUS_FRUIT)
        {

        }

        public override string Name
        {
            get
            {
                return "ChorusFruit";
            }
        }

        public override int FoodRestore
        {
            get
            {
                return 4;
            }
        }

        public override float SaturationRestore
        {
            get
            {
                return 2.4f;
            }
        }
    }
}
