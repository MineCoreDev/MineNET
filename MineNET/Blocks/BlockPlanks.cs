namespace MineNET.Blocks
{
    public class BlockPlanks : Block
    {
        public BlockPlanks() : base(BlockIDs.PLANKS, "Planks")
        {
            this.Hardness = 2.0f;
            this.Resistance = 15f;
        }
    }
}
