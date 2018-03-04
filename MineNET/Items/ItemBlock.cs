
using MineNET.Blocks;

namespace MineNET.Items
{
    public class ItemBlock : Item
    {
        public ItemBlock(Block block) : base(block.ID)
        {
            this.Block = block;
        }

        public override string Name
        {
            get
            {
                return this.Block.Name;
            }
        }
    }
}
