namespace MineNET.Blocks
{
    public class BlockRedstoneOreLit : Block
    {
        public BlockRedstoneOreLit() : base(BlockFactory.LIT_REDSTONE_ORE)
        {

        }

        public override string Name
        {
            get
            {
                return "LitRedstoneTorch";
            }
        }
    }
}
