using MineNET.Blocks;

namespace MineNET.Items
{
    public class ItemDoorDarkOak : Item
    {
        public ItemDoorDarkOak() : base(ItemFactory.DARK_OAK_DOOR)
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
