namespace MineNET.Items
{
    public class ItemRabbitStew : ItemFood
    {
        public ItemRabbitStew() : base(ItemFactory.RABBIT_STEW)
        {

        }

        public override string Name
        {
            get
            {
                return "RabbitStew";
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
                return 10;
            }
        }

        public override float SaturationRestore
        {
            get
            {
                return 12f;
            }
        }
    }
}
