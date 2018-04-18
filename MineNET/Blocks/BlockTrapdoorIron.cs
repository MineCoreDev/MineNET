namespace MineNET.Blocks
{
    public class BlockTrapdoorIron : BlockTrapdoor
    {
        public BlockTrapdoorIron() : base(BlockFactory.IRON_TRAPDOOR)
        {

        }

        public override string Name
        {
            get
            {
                return "IronTrapdoor";
            }
        }
    }
}
