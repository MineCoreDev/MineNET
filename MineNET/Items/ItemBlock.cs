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

        public override int ID
        {
            get
            {
                return this.block.ID < 256 ? this.block.ID : -this.block.ID + 255;
            }
        }

        public override string GetName(int damage)
        {
            return this.Block.Name;
        }
    }
}
