namespace MineNET.Items
{
    public class ItemGoldenPickaxe : ItemTool
    {
        public ItemGoldenPickaxe() : base(ItemFactory.GOLDEN_PICKAXE)
        {

        }

        public override string Name
        {
            get
            {
                return "GoldenPickaxe";
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
                return 33;
            }
        }
    }
}
