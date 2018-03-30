using MineNET.Blocks;

namespace MineNET.Items
{
    public class ItemCauldron : Item
    {
        public ItemCauldron() : base(ItemFactory.CAULDRON)
        {
            this.Block = new BlockCauldron();
        }

        public override string Name
        {
            get
            {
                return "Cauldron";
            }
        }
    }
}
