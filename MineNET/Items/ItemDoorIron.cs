using MineNET.Blocks;

namespace MineNET.Items
{
    public class ItemDoorIron : ItemDoor
    {
        public ItemDoorIron() : base(ItemFactory.IRON_DOOR)
        {
            this.Block = new BlockDoorIron();
        }

        public override string Name
        {
            get
            {
                return "IronDoor";
            }
        }
    }
}
