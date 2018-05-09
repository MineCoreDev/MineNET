namespace MineNET.Blocks
{
    public class BlockButtonWooden : BlockButtonBase
    {
        public BlockButtonWooden() : base(BlockFactory.WOODEN_BUTTON)
        {

        }

        public override string Name
        {
            get
            {
                return "WoodenButton";
            }
        }
    }
}
