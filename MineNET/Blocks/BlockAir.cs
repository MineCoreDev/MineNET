namespace MineNET.Blocks
{
    public class BlockAir : BlockTransparent
    {
        public BlockAir() : base(BlockFactory.AIR)
        {

        }

        public override string Name
        {
            get
            {
                return "Air";
            }
        }

        public override bool CanBePlaced
        {
            get
            {
                return false;
            }
        }

        public override bool CanBeReplaced
        {
            get
            {
                return true;
            }
        }
    }
}
