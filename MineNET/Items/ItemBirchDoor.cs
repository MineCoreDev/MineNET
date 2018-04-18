using MineNET.Blocks;

namespace MineNET.Items
{
    public class ItemBirchDoor : Item
    {
        public ItemBirchDoor() : base(ItemFactory.BIRCH_DOOR)
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
