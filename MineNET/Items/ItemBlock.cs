using MineNET.Blocks;

namespace MineNET.Items
{
    public class ItemBlock : Item
    {
        public ItemBlock(Block block) : base(block.Name, block.ID < 256 ? block.ID : -block.ID + 255)
        {
            this.Block = block.Clone();
        }
    }
}
