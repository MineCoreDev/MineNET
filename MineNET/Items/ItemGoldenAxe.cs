namespace MineNET.Items
{
    public class ItemGoldenAxe : ItemTool
    {
        public ItemGoldenAxe() : base(ItemFactory.GOLDEN_AXE)
        {

        }

        public override string Name
        {
            get
            {
                return "GoldenAxe";
            }
        }

        public override bool IsAxe
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
