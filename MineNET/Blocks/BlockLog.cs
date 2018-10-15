using MineNET.Items;

namespace MineNET.Blocks
{
    public class BlockLog : BlockSolid
    {
        public BlockLog() : base(BlockIDs.LOG, "Log")
        {
            this.Hardness = 2f;
            this.Resistance = 10f;
            this.ToolType = ItemToolType.AXE;
        }
    }
}
