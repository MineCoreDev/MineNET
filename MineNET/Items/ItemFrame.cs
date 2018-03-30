using MineNET.Blocks;

namespace MineNET.Items
{
    public class ItemFrame : Item
    {
        public ItemFrame() : base(ItemFactory.ITEM_FRAME)
        {
            this.Block = new BlockFrame();
        }

        public override string Name
        {
            get
            {
                return "ItemFrame";
            }
        }
    }
}
