namespace MineNET.Items
{
    public class ItemMutton : ItemFood
    {
        public ItemMutton() : base(ItemFactory.MUTTON)
        {

        }

        public override string Name
        {
            get
            {
                return "Mutton";
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
