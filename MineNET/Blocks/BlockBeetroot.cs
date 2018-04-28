namespace MineNET.Blocks
{
    public class BlockBeetroot : BlockTransparent
    {
        public BlockBeetroot() : base(BlockFactory.BEETROOT)
        {

        }

        public override string Name
        {
            get
            {
                return "Beetroot";
            }
        }
    }
}
