namespace MineNET.Items
{
    public class ItemStoneShovel : ItemTool
    {
        public ItemStoneShovel() : base(ItemFactory.STONE_SHOVEL)
        {

        }

        public override string Name
        {
            get
            {
                return "StoneShovel";
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
                return 132;
            }
        }
    }
}
