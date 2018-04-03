namespace MineNET.Items
{
    public class ItemBeef : ItemFood
    {
        public ItemBeef() : base(ItemFactory.BEEF)
        {

        }

        public override string Name
        {
            get
            {
                return "Beef";
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
                return 1.8f;
            }
        }
    }
}
