namespace MineNET.Items
{
    public class ItemClownfish : ItemFood
    {
        public ItemClownfish() : base(ItemFactory.CLOWNFISH)
        {

        }

        public override string Name
        {
            get
            {
                return "Clownfish";
            }
        }

        public override int FoodRestore
        {
            get
            {
                return 1;
            }
        }

        public override float SaturationRestore
        {
            get
            {
                return 0.2f;
            }
        }
    }
}
