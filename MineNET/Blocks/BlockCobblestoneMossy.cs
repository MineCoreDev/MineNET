namespace MineNET.Blocks
{
    public class BlockCobblestoneMossy : BlockSolid
    {
        public BlockCobblestoneMossy() : base(BlockFactory.MOSSY_COBBLESTONE)
        {

        }

        public override string Name
        {
            get
            {
                return "MossyCobblestone";
            }
        }
    }
}
