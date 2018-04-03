namespace MineNET.Blocks
{
    public class BlockGrass : BlockSolid
    {
        public BlockGrass() : base(BlockFactory.GRASS)
        {

        }

        public override string Name
        {
            get
            {
                return "Grass";
            }
        }
    }
}
