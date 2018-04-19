using MineNET.Blocks;

namespace MineNET.Items
{
    public class ItemDoorBirch : Item
    {
        public ItemDoorBirch() : base(ItemFactory.BIRCH_DOOR)
        {
            this.Block = new BlockDoorBirch();
        }

        public override string Name
        {
            get
            {
                return "BirchDoor";
            }
        }
    }
}
