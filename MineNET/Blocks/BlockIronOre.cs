using MineNET.Items;

namespace MineNET.Blocks
{
    public class BlockIronOre : BlockOre
    {
        public BlockIronOre() : base(BlockIDs.IRON_ORE, "IronOre")
        {
            this.Hardness = 3f;
            this.Resistance = 15;
            this.ToolType = ItemToolType.PICKAXE;
        }
    }
}
