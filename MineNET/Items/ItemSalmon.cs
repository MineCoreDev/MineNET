namespace MineNET.Items
{
    public class ItemSalmon : ItemFood
    {
        public ItemSalmon() : base(ItemFactory.SALMON)
        {

        }

        public override string Name
        {
            get
            {
                return "Salmon";
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
                return 0.4f;
            }
        }
    }
}
