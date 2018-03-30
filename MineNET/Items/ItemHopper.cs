using MineNET.Blocks;

namespace MineNET.Items
{
    public class ItemHopper : Item
    {
        public ItemHopper() : base(ItemFactory.HOPPER)
        {
            this.Block = new BlockHopper();
        }

        public override string Name
        {
            get
            {
                return "Hopper";
            }
        }
    }
}
