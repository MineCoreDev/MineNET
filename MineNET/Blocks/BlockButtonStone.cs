namespace MineNET.Blocks
{
    public class BlockButtonStone : BlockButtonBase
    {
        public BlockButtonStone() : base(BlockFactory.STONE_BUTTON)
        {

        }

        public override string Name
        {
            get
            {
                return "StoneButton";
            }
        }
    }
}
