namespace MineNET.Items
{
    public class ItemDiamondAxe : ItemTool
    {
        public ItemDiamondAxe() : base(ItemFactory.DIAMOND_AXE)
        {

        }

        public override string Name
        {
            get
            {
                return "DiamondAxe";
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
                return 1562;
            }
        }
    }
}
