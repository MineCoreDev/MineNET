namespace MineNET.Items
{
    public class ItemGoldenApple : ItemFood
    {
        public ItemGoldenApple() : base(ItemFactory.GOLDEN_APPLE)
        {

        }

        public override string Name
        {
            get
            {
                return "GoldenApple";
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
                return 9.6f;
            }
        }
    }
}
