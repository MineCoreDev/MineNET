using MineNET.Blocks;

namespace MineNET.Items
{
    public class ItemSign : Item
    {
        public override int ID { get; } = ItemIDs.SIGN;

        public override string Name => "Sign";

        public override byte MaxStackSize => 16;

        public override Block Block => Block.Get(BlockIDs.STANDING_SIGN);
    }
}
