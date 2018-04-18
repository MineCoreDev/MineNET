namespace MineNET.Blocks
{
    public class BlockShulkerBoxUndyed : Block
    {
        public BlockShulkerBoxUndyed() : base(BlockFactory.UNDYED_SHULKER_BOX)
        {

        }

        public override string Name
        {
            get
            {
                return "UndyedShulkerBox";
            }
        }
    }
}
