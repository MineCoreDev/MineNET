namespace MineNET.Items
{
    public class ItemBeetroot : ItemFood
    {
        public ItemBeetroot() : base(ItemFactory.BEETROOT)
        {

        }

        public override string Name
        {
            get
            {
                return "Beetroot";
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
                return 1.2f;
            }
        }
    }
}
