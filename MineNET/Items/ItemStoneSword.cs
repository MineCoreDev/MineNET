namespace MineNET.Items
{
    public class ItemStoneSword : ItemTool
    {
        public ItemStoneSword() : base(ItemFactory.STONE_SWORD)
        {

        }

        public override string Name
        {
            get
            {
                return "StoneSword";
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
                return 132;
            }
        }
    }
}
