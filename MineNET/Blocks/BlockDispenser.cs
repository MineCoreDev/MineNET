using MineNET.Items;

namespace MineNET.Blocks
{
    public class BlockDispenser : BlockSolid
    {
        public BlockDispenser() : base(BlockIDs.DISPENSER, "Dispenser")
        {
            this.Hardness = 3.5f;
            this.Resistance = 17.5f;
            this.ToolType = ItemToolType.PICKAXE;
        }
    }
}
