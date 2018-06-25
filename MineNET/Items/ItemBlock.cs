using MineNET.Blocks;

namespace MineNET.Items
{
    public class ItemBlock : Item
    {
        public Block Block { get; }

        public ItemBlock(Block block) : base(block.Name, block.ID, block.Damage)
        {
            this.Block = block.Clone();
        }
    }
}
