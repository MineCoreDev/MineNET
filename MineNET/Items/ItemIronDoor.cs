using MineNET.Blocks;

namespace MineNET.Items
{
    public class ItemIronDoor : Item
    {
        public override int ID => ItemIDs.IRON_DOOR;

        public override string Name => "Iron Door";

        public override Block Block => Block.Get(BlockIDs.IRON_DOOR);
    }
}
