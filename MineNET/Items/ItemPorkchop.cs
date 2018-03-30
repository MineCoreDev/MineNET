namespace MineNET.Items
{
    public class ItemPorkchop : ItemFood
    {
        public ItemPorkchop() : base(ItemFactory.PORKCHOP)
        {

        }

        public override string Name
        {
            get
            {
                return "Porkchop";
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
