namespace MineNET.Blocks
{
    public class BlockAir : BlockTransparent
    {
        public BlockAir() : base(BlockIDs.AIR, "Air")
        {
            this.CanBePlaced = false;
            this.CanBeReplaced = true;
            this.CanBreak = false;
        }
    }
}
