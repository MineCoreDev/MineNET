using MineNET.Entities.Players;
using MineNET.Items;

namespace MineNET.Blocks
{
    public class BlockCake : Block
    {
        public BlockCake() : base(BlockFactory.CAKE)
        {

        }

        public override string Name
        {
            get
            {
                return "Cake";
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
