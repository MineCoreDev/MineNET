using MineNET.Blocks.Data;
using MineNET.Entities.Players;
using MineNET.Items;
using MineNET.Values;

namespace MineNET.Blocks
{
    public class BlockBeacon : BlockSolid
    {
        public BlockBeacon() : base(BlockFactory.BEACON)
        {

        }

        public override string Name
        {
            get
            {
                return "Beacon";
            }
        }

        public override bool Place(Block clicked, Block replace, BlockFace face, Vector3 clickPos, Player player, Item item)
        {
            return base.Place(clicked, replace, face, clickPos, player, item);
        }

        public override bool Break(Player player, Item item)
        {
            return base.Break(player, item);
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
