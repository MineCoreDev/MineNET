using MineNET.Blocks;

namespace MineNET.Items
{
    public class ItemAcaciaDoor : Item
    {
        public ItemAcaciaDoor() : base(ItemFactory.ACACIA_DOOR)
        {
            this.Block = new BlockAcaciaDoor();
        }

        public override string Name
        {
            get
            {
                return "AcaciaDoor";
            }
        }
    }
}
