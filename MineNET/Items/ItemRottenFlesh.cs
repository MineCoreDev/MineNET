namespace MineNET.Items
{
    public class ItemRottenFlesh : ItemFood
    {
        public ItemRottenFlesh() : base(ItemFactory.ROTTEN_FLESH)
        {

        }

        public override string Name
        {
            get
            {
                return "RottemFlesh";
            }
        }

        public override int FoodRestore
        {
            get
            {
                return 4;
            }
        }

        public override float SaturationRestore
        {
            get
            {
                return 0.8f;
            }
        }
    }
}
