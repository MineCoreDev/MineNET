using MineNET.Blocks;

namespace MineNET.Items
{
    public class ItemDarkOakDoor : Item
    {
        public ItemDarkOakDoor() : base(ItemFactory.DARK_OAK_DOOR)
        {
            this.Block = new BlockDarkOakDoor();
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
