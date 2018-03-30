using MineNET.Blocks;

namespace MineNET.Items
{
    public class ItemSpruceDoor : Item
    {
        public ItemSpruceDoor() : base(ItemFactory.SPRUCE_DOOR)
        {
            this.Block = new BlockSpruceDoor();
        }

        public override string Name
        {
            get
            {
                return "SpruceDoor";
            }
        }
    }
}
