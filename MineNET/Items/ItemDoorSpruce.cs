using MineNET.Blocks;

namespace MineNET.Items
{
    public class ItemDoorSpruce : Item
    {
        public ItemDoorSpruce() : base(ItemFactory.SPRUCE_DOOR)
        {
            this.Block = new BlockDoorSpruce();
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
