namespace MineNET.Items
{
    public class ItemStoneAxe : ItemTool
    {
        public ItemStoneAxe() : base(ItemFactory.STONE_AXE)
        {

        }

        public override string Name
        {
            get
            {
                return "StoneAxe";
            }
        }

        public override bool IsAxe
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
