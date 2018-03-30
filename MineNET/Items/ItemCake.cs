using MineNET.Blocks;

namespace MineNET.Items
{
    public class ItemCake : Item
    {
        public ItemCake() : base(ItemFactory.CAKE)
        {
            this.Block = new BlockCake();
        }

        public override string Name
        {
            get
            {
                return "Cake";
            }
        }
    }
}
