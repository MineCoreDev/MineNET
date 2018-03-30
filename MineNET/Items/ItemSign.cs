using MineNET.Blocks;

namespace MineNET.Items
{
    public class ItemSign : Item
    {
        public ItemSign() : base(ItemFactory.SIGN)
        {
            this.Block = new BlockStandingSign();
        }

        public override string Name
        {
            get
            {
                return "Sign";
            }
        }

        public override byte MaxStackSize
        {
            get
            {
                return 16;
            }
        }
    }
}
