using MineNET.Blocks;

namespace MineNET.Items
{
    public class ItemBed : Item
    {
        public ItemBed() : base(ItemFactory.BED)
        {
            this.Block = new BlockBed();
        }

        public override string Name
        {
            get
            {
                return "Bed";
            }
        }

        public override byte MaxStackSize
        {
            get
            {
                return 1;
            }
        }
    }
}
