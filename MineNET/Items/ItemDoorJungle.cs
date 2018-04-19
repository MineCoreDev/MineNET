using MineNET.Blocks;

namespace MineNET.Items
{
    public class ItemDoorJungle : Item
    {
        public ItemDoorJungle() : base(ItemFactory.JUNGLE_DOOR)
        {
            this.Block = new BlockDoorJungle();
        }

        public override string Name
        {
            get
            {
                return "JungleDoor";
            }
        }
    }
}
