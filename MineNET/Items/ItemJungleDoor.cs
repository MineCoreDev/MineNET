using MineNET.Blocks;

namespace MineNET.Items
{
    public class ItemJungleDoor : Item
    {
        public ItemJungleDoor() : base(ItemFactory.JUNGLE_DOOR)
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
