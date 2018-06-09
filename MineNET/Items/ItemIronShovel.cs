namespace MineNET.Items
{
    public class ItemIronShovel : ItemTool
    {
        public ItemIronShovel() : base(ItemFactory.IRON_SHOVEL)
        {

        }

        public override string Name
        {
            get
            {
                return "IronShovel";
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
                return 251;
            }
        }
    }
}
