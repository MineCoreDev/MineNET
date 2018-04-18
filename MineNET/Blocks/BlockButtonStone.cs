namespace MineNET.Blocks
{
    public class BlockButtonStone : BlockTransparent
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
