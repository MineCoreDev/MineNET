namespace MineNET.Items
{
    public class ItemFishingRod : ItemTool
    {
        public ItemFishingRod() : base(ItemFactory.FISHING_ROD)
        {

        }

        public override string Name
        {
            get
            {
                return "FishingRod";
            }
        }

        public override byte MaxStackSize
        {
            get
            {
                return 1;
            }
        }

        public override int MaxDurability
        {
            get
            {
                return 65;
            }
        }
    }
}
