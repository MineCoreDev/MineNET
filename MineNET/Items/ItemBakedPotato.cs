namespace MineNET.Items
{
    public class ItemBakedPotato : ItemFood
    {
        public ItemBakedPotato() : base(ItemFactory.BAKED_POTATO)
        {

        }

        public override string Name
        {
            get
            {
                return "BakedPotato";
            }
        }

        public override int FoodRestore
        {
            get
            {
                return 5;
            }
        }

        public override float SaturationRestore
        {
            get
            {
                return 6f;
            }
        }
    }
}
