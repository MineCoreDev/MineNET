using MineNET.Blocks;

namespace MineNET.Items
{
    public class ItemBrewingStand : Item
    {
        public ItemBrewingStand() : base(ItemFactory.BREWING_STAND)
        {
            this.Block = new BlockBrewingStand();
        }

        public override string Name
        {
            get
            {
                return "BrewingStand";
            }
        }
    }
}
