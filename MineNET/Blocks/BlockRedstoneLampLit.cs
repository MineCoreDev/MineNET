namespace MineNET.Blocks
{
    public class BlockRedstoneLampLit : BlockSolid
    {
        public BlockRedstoneLampLit() : base(BlockFactory.LIT_REDSTONE_LAMP)
        {

        }

        public override string Name
        {
            get
            {
                return "LitRedstoneLamp";
            }
        }
    }
}
