using MineNET.Blocks;

namespace MineNET.Items
{
    public class ItemFlowerPot : Item
    {
        public ItemFlowerPot() : base(ItemFactory.FLOWER_POT)
        {
            this.Block = new BlockFlowerPot();
        }

        public override string Name
        {
            get
            {
                return "FlowerPot";
            }
        }
    }
}
