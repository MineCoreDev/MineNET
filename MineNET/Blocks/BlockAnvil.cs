using MineNET.Entities.Players;
using MineNET.Items;

namespace MineNET.Blocks
{
    public class BlockAnvil : Block
    {
        public BlockAnvil() : base(BlockFactory.ANVIL)
        {

        }

        public override string Name
        {
            get
            {
                return "Anvil";
            }
        }

        public override bool Activate(Player player, Item item)
        {
            return base.Activate(player, item);
        }

        public override bool CanBeActivated
        {
            get
            {
                return true;
            }
        }
    }
}
