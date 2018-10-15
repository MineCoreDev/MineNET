using MineNET.Items;

namespace MineNET.Blocks
{
    public class BlockDirt : BlockSolid
    {
        public BlockDirt() : base(BlockIDs.DIRT, "Dirt")
        {
            this.Hardness = 0.5f;
            this.Resistance = 2.5f;
            this.ToolType = ItemToolType.SHOVEL;
        }
    }
}
