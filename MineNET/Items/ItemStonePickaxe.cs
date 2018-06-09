namespace MineNET.Items
{
    public class ItemStonePickaxe : ItemTool
    {
        public ItemStonePickaxe() : base(ItemFactory.STONE_PICKAXE)
        {

        }

        public override string Name
        {
            get
            {
                return "StonePickaxe";
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
                return 132;
            }
        }
    }
}
