namespace MineNET.Items
{
    public class ItemIronSword : ItemTool
    {
        public ItemIronSword() : base(ItemFactory.IRON_SWORD)
        {

        }

        public override string Name
        {
            get
            {
                return "IronSword";
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
                return 251;
            }
        }
    }
}
