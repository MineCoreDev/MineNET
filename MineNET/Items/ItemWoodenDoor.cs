using MineNET.Blocks;

namespace MineNET.Items
{
    public class ItemWoodenDoor : Item
    {
        public override int ID { get; } = ItemIDs.WOODEN_DOOR;

        public override string GetName(int damage)
        {
            return "Wooden Door";
        }

        public override Block Block
        {
            get
            {
                return Block.Get(BlockIDs.WOODEN_DOOR);
            }
        }
    }
}
