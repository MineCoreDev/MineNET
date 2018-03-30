namespace MineNET.Items
{
    public class ItemPufferfish : ItemFood
    {
        public ItemPufferfish() : base(ItemFactory.PUFFERFISH)
        {

        }

        public override string Name
        {
            get
            {
                return "Pufferfish";
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
