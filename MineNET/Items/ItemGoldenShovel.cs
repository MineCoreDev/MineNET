namespace MineNET.Items
{
    public class ItemGoldenShovel : ItemTool
    {
        public ItemGoldenShovel() : base(ItemFactory.GOLDEN_SHOVEL)
        {

        }

        public override string Name
        {
            get
            {
                return "GoldenShovel";
            }
        }

        public override bool IsShovel
        {
            get
            {
                return true;
            }
        }

        public override int MaxDurability
        {
            get
            {
                return 33;
            }
        }
    }
}
