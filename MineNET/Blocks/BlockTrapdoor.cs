namespace MineNET.Blocks
{
    public class BlockTrapdoor : Block
    {
        public BlockTrapdoor() : base(BlockFactory.TRAPDOOR)
        {

        }

        public BlockTrapdoor(int id) : base(id)
        {

        }

        public override string Name
        {
            get
            {
                return "Trapdoor";
            }
        }
    }
}
