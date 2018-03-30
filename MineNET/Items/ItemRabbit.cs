namespace MineNET.Items
{
    public class ItemRabbit : ItemFood
    {
        public ItemRabbit() : base(ItemFactory.RABBIT)
        {

        }

        public override string Name
        {
            get
            {
                return "Rabbit";
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
