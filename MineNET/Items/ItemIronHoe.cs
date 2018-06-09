namespace MineNET.Items
{
    public class ItemIronHoe : ItemTool
    {
        public ItemIronHoe() : base(ItemFactory.IRON_HOE)
        {

        }

        public override string Name
        {
            get
            {
                return "IronHoe";
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
                return 251;
            }
        }
    }
}
