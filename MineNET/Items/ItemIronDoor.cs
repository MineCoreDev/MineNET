using MineNET.Blocks;

namespace MineNET.Items
{
    public class ItemIronDoor : Item
    {
        public override int ID { get; } = ItemIDs.IRON_DOOR;

        public override string GetName(int damage)
        {
            return "Iron Door";
        }

        public override Block Block
        {
            get
            {
                return Block.Get(BlockIDs.IRON_DOOR);
            }
        }
    }
}
