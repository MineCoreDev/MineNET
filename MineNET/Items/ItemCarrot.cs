namespace MineNET.Items
{
    public class ItemCarrot : ItemFood
    {
        public ItemCarrot() : base(ItemFactory.CARROT)
        {

        }

        public override string Name
        {
            get
            {
                return "Carrot";
            }
        }

        public override int FoodRestore
        {
            get
            {
                return 3;
            }
        }

        public override float SaturationRestore
        {
            get
            {
                return 3.6f;
            }
        }
    }
}
