namespace MineNET.Blocks
{
    public class BlockClayHardened : BlockSolid
    {
        public BlockClayHardened() : base(BlockFactory.HARDENED_CLAY)
        {

        }

        public override string Name
        {
            get
            {
                return "HardenedClay";
            }
        }
    }
}
