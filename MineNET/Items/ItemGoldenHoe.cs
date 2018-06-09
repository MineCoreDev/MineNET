namespace MineNET.Items
{
    public class ItemGoldenHoe : ItemTool
    {
        public ItemGoldenHoe() : base(ItemFactory.GOLDEN_HOE)
        {

        }

        public override string Name
        {
            get
            {
                return "GoldenHoe";
            }
        }

        public override bool IsHoe
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
