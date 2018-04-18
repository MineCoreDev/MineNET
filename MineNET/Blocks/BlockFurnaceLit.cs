namespace MineNET.Blocks
{
    public class BlockFurnaceLit : BlockSolid
    {
        public BlockFurnaceLit() : base(BlockFactory.LIT_FURNACE)
        {

        }

        public override string Name
        {
            get
            {
                return "LitFurnace";
            }
        }
    }
}
