namespace MineNET.Blocks
{
    public class BlockRedstoneTorchUnlit : Block
    {
        public BlockRedstoneTorchUnlit() : base(BlockFactory.UNLIT_REDSTONE_TORCH)
        {

        }

        public override string Name
        {
            get
            {
                return "UnlitRedstoneTorch";
            }
        }
    }
}
