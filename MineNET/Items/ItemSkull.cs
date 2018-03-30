using MineNET.Blocks;

namespace MineNET.Items
{
    public class ItemSkull : Item
    {
        public ItemSkull() : base(ItemFactory.SKULL)
        {
            this.Block = new BlockSkull();
        }

        public override string Name
        {
            get
            {
                return "Skull";
            }
        }
    }
}
