namespace MineNET.Items
{
    public class ItemStoneHoe : ItemTool
    {
        public ItemStoneHoe() : base(ItemFactory.STONE_HOE)
        {

        }

        public override string Name
        {
            get
            {
                return "StoneHoe";
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
                return 132;
            }
        }
    }
}
