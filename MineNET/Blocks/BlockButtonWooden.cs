using MineNET.Items;

namespace MineNET.Blocks
{
    public class BlockButtonWooden : Block
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

        public override Item Item
        {
            get
            {
                return Item.Get(BlockFactory.WOODEN_BUTTON, 0, 1);
            }
        }
    }
}
