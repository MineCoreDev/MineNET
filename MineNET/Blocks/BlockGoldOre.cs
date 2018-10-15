using MineNET.Items;

namespace MineNET.Blocks
{
    public class BlockGoldOre : BlockOre
    {
        public BlockGoldOre() : base(BlockIDs.GOLD_ORE, "GoldOre")
        {
            this.Hardness = 3f;
            this.Resistance = 15;
            this.ToolType = ItemToolType.PICKAXE;
        }
    }
}
