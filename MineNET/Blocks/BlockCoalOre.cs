using MineNET.Items;

namespace MineNET.Blocks
{
    public class BlockCoalOre : BlockOre
    {
        public BlockCoalOre() : base(BlockIDs.COAL_ORE, "CoalOre")
        {
            this.Hardness = 3f;
            this.Resistance = 15;
            this.ToolType = ItemToolType.PICKAXE;
        }
    }
}
