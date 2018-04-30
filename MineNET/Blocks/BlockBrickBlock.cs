namespace MineNET.Blocks
{
    public class BlockBrickBlock : BlockSolid
    {
        public BlockBrickBlock() : base(BlockFactory.BRICK_BLOCK)
        {

        }

        public override string Name
        {
            get
            {
                return "BrickBlock";
            }
        }
    }
}
