using MineNET.Blocks;

namespace MineNET.Items
{
    public class ItemSign : Item
    {
        public override int ID { get; } = ItemIDs.SIGN;

        public override string GetName(int damage)
        {
            return "Sign";
        }

        public override byte MaxStackSize
        {
            get
            {
                return 16;
            }
        }

        public override Block Block
        {
            get
            {
                return Block.Get(BlockIDs.STANDING_SIGN);
            }
        }
    }
}
