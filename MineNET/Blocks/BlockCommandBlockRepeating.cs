namespace MineNET.Blocks
{
    public class BlockCommandBlockRepeating : BlockSolid
    {
        public BlockCommandBlockRepeating() : base(BlockFactory.REPEATING_COMMAND_BLOCK)
        {

        }

        public override string Name
        {
            get
            {
                return "RepeatingCommandBlock";
            }
        }
    }
}
