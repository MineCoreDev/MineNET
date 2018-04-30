using MineNET.Items;

namespace MineNET.Blocks
{
    public class BlockBrewingStand : Block
    {
        public BlockBrewingStand() : base(BlockFactory.BREWING_STAND)
        {

        }

        public override string Name
        {
            get
            {
                return "BrewingStand";
            }
        }

        public override Item Item
        {
            get
            {
                return Item.Get(ItemFactory.BREWING_STAND, 0, 1);
            }
        }
    }
}
