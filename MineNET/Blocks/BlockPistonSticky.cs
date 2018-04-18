namespace MineNET.Blocks
{
    public class BlockPistonSticky : BlockSolid
    {
        public BlockPistonSticky() : base(BlockFactory.STICKY_PISTON)
        {

        }

        public override string Name
        {
            get
            {
                return "StickyPiston";
            }
        }
    }
}
