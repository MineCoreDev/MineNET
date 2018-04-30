using MineNET.Items;

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

        public override Item Item
        {
            get
            {
                return Item.Get(BlockFactory.STONE_BUTTON, 0, 1);
            }
        }
    }
}
