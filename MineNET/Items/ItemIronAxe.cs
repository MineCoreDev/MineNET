namespace MineNET.Items
{
    public class ItemIronAxe : ItemTool
    {
        public ItemIronAxe() : base(ItemFactory.IRON_AXE)
        {

        }

        public override string Name
        {
            get
            {
                return "IronAxe";
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
                return 251;
            }
        }
    }
}
