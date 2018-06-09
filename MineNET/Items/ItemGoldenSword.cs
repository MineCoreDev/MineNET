namespace MineNET.Items
{
    public class ItemGoldenSword : ItemTool
    {
        public ItemGoldenSword() : base(ItemFactory.GOLDEN_SWORD)
        {

        }

        public override string Name
        {
            get
            {
                return "GoldenSword";
            }
        }

        public override bool IsSword
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
