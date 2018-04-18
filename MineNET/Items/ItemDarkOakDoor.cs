using MineNET.Blocks;

namespace MineNET.Items
{
    public class ItemDarkOakDoor : Item
    {
        public ItemDarkOakDoor() : base(ItemFactory.DARK_OAK_DOOR)
        {
            this.Block = new BlockDoorDarkOak();
        }

        public override string Name
        {
            get
            {
                return "DarkOakDoor";
            }
        }
    }
}
