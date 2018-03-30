using MineNET.Blocks;

namespace MineNET.Items
{
    public class ItemRedstone : Item
    {
        public ItemRedstone() : base(ItemFactory.REDSTONE)
        {
            this.Block = new BlockRedstoneBlock();
        }

        public override string Name
        {
            get
            {
                return "Redstone";
            }
        }
    }
}
