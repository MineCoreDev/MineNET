using MineNET.Items;

namespace MineNET.Blocks
{
    public class BlockGravel : BlockFalling
    {
        public BlockGravel() : base(BlockIDs.GRAVEL, "Gravel")
        {
            this.IsSolid = true;

            this.Hardness = 0.6f;
            this.Resistance = 3f;
            this.ToolType = ItemToolType.SHOVEL;
        }
    }
}
