namespace MineNET.Items
{
    public class ItemDiamondPickaxe : ItemTool
    {
        public ItemDiamondPickaxe() : base(ItemFactory.DIAMOND_PICKAXE)
        {

        }

        public override string Name
        {
            get
            {
                return "DiamondPickaxe";
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
                return 1562;
            }
        }
    }
}
