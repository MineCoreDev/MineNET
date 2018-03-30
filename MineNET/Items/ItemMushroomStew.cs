namespace MineNET.Items
{
    public class ItemMushroomStew : ItemFood
    {
        public ItemMushroomStew() : base(ItemFactory.MUSHROOM_STEW)
        {

        }

        public override string Name
        {
            get
            {
                return "MushroomStew";
            }
        }

        public override byte MaxStackSize
        {
            get
            {
                return 1;
            }
        }

        public override int FoodRestore
        {
            get
            {
                return 6;
            }
        }

        public override float SaturationRestore
        {
            get
            {
                return 7.2f;
            }
        }
    }
}
