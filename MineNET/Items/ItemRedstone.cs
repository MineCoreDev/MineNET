using MineNET.Blocks;

namespace MineNET.Items
{
    public class ItemRedstone : Item
    {
        public override int ID => ItemIDs.REDSTONE;

        public override string Name => "Redstone";

        public override Block Block => Block.Get(BlockIDs.REDSTONE_WIRE);
    }
}
