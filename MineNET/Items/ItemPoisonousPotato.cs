namespace MineNET.Items
{
    public class ItemPoisonousPotato : ItemFood
    {
        public ItemPoisonousPotato() : base(ItemFactory.POISONOUS_POTATO)
        {

        }

        public override string Name
        {
            get
            {
                return "PoisonousPotato";
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
