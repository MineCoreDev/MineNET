using MineNET.Blocks;

namespace MineNET.Items
{
    public class ItemWoodenDoor : Item
    {
        public override int ID => ItemIDs.WOODEN_DOOR;

        public override string Name => "Wooden Door";

        public override Block Block => Block.Get(BlockIDs.WOODEN_DOOR);
    }
}
