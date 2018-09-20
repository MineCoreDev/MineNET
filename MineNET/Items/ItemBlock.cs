using MineNET.Blocks;

namespace MineNET.Items
{
    public class ItemBlock : Item
    {
        public ItemBlock(Block block) : base(block.Name, block.ID)
        {
            this.Block = block.Clone();
        }
    }
}
