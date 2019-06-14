using MineNET.Blocks;

namespace MineNET.Items
{
    public class ItemRedstone : Item
    {
        public override int ID { get; } = ItemIDs.REDSTONE;

        public override string GetName(int damage)
        {
            return "Redstone";
        }

        public override Block Block
        {
            get
            {
                return Block.Get(BlockIDs.REDSTONE_WIRE);
            }
        }
    }
}
