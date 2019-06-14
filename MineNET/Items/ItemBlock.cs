using MineNET.Blocks;

namespace MineNET.Items
{
    public class ItemBlock : Item
    {
        private Block block;

        public ItemBlock(Block block)
        {
            this.block = block;
        }

        public override int ID => this.block.ID < 256 ? this.block.ID : -this.block.ID + 255;

        public override string Name => this.block.Name;

        public override Block Block => this.block.Clone();
    }
}
