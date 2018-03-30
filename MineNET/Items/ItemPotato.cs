namespace MineNET.Items
{
    public class ItemPotato : ItemFood
    {
        public ItemPotato() : base(ItemFactory.POTATO)
        {

        }

        public override string Name
        {
            get
            {
                return "Potato";
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
                return 0.6f;
            }
        }
    }
}
