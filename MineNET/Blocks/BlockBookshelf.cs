namespace MineNET.Blocks
{
    public class BlockBookshelf : BlockSolid
    {
        public BlockBookshelf() : base(BlockFactory.BOOKSHELF)
        {

        }

        public override string Name
        {
            get
            {
                return "Bookshelf";
            }
        }
    }
}
