namespace MineNET.Items
{
    public class ItemWoodenPickaxe : ItemTool
    {
        public ItemWoodenPickaxe() : base(ItemFactory.WOODEN_PICKAXE)
        {

        }

        public override string Name
        {
            get
            {
                return "WoodenPickaxe";
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
                return 60;
            }
        }
    }
}
