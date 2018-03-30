namespace MineNET.Items
{
    public class ItemChicken : ItemFood
    {
        public ItemChicken() : base(ItemFactory.CHICKEN)
        {

        }

        public override string Name
        {
            get
            {
                return "Chicken";
            }
        }

        public override int FoodRestore
        {
            get
            {
                return 2;
            }
        }

        public override float SaturationRestore
        {
            get
            {
                return 1.2f;
            }
        }
    }
}
