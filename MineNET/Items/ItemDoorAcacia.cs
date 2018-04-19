using MineNET.Blocks;

namespace MineNET.Items
{
    public class ItemDoorAcacia : Item
    {
        public ItemDoorAcacia() : base(ItemFactory.ACACIA_DOOR)
        {
            this.Block = new BlockDoorAcacia();
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
