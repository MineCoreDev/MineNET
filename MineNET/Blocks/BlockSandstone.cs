namespace MineNET.Blocks
{
    public class BlockSandstone : BlockSolid
    {
        public BlockSandstone() : base(BlockIDs.SANDSTONE, "Sandstone")
        {

        }

        public override string Name
        {
            get
            {
                if (this.Damage == 1)
                {
                    return "ChiseledSandstone";
                }
                else if (this.Damage == 2)
                {
                    return "SmoothSandstone";
                }
                return "Sandstone";
            }
        }
    }
}
