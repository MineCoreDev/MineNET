namespace MineNET.Blocks
{
    public class BlockSponge : BlockSolid
    {
        public BlockSponge() : base(BlockIDs.SPONGE, "Sponge")
        {
            this.Hardness = 0.6f;
            this.Resistance = 3f;
        }
    }
}
