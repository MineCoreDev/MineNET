namespace MineNET.Items
{
    public class ItemWoodenAxe : ItemTool
    {
        public ItemWoodenAxe() : base(ItemFactory.WOODEN_AXE)
        {

        }

        public override string Name
        {
            get
            {
                return "WoodenAxe";
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
                return 60;
            }
        }
    }
}
