using MineNET.Blocks;

namespace MineNET.Items
{
    public class ItemString : Item
    {
        public ItemString() : base(ItemFactory.STRING)
        {
            this.Block = new BlockTripwire();
        }

        public override string Name
        {
            get
            {
                return "String";
            }
        }
    }
}
