using MineNET.Blocks;

namespace MineNET.Items
{
    public class ItemRepeater : Item
    {
        public ItemRepeater() : base(ItemFactory.REPEATER)
        {
            this.Block = new BlockRepeaterUnpowered();
        }

        public override string Name
        {
            get
            {
                return "Repeater";
            }
        }
    }
}
