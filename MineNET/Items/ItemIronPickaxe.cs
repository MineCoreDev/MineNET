namespace MineNET.Items
{
    public class ItemIronPickaxe : ItemTool
    {
        public ItemIronPickaxe() : base(ItemFactory.IRON_PICKAXE)
        {

        }

        public override string Name
        {
            get
            {
                return "IronPickaxe";
            }
        }

        public override bool IsPickaxe
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
