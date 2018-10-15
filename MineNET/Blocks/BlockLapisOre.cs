using MineNET.Items;

namespace MineNET.Blocks
{
    public class BlockLapisOre : BlockOre
    {
        public BlockLapisOre() : base(BlockIDs.LAPIS_ORE, "LapisOre")
        {
            this.Hardness = 3f;
            this.Resistance = 15f;
            this.ToolType = ItemToolType.PICKAXE;
            this.ToolTier = ItemToolTier.STONE;
        }
    }
}
