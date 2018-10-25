using MineNET.Data;
using MineNET.Entities.Players;
using MineNET.Items;
using MineNET.Values;

namespace MineNET.Blocks
{
    public class BlockChest : BlockContainer
    {
        public BlockChest() : base(BlockIDs.CHEST, "Chest")
        {
            this.Hardness = 2.5f;
            this.Resistance = 12.5f;
            this.ToolType = ItemToolType.PICKAXE;

            this.CanBeActivated = true;
        }

        public override bool Place(Block clicked, Block replace, BlockFace face, Vector3 clickPos, Player player, ItemStack item)
        {
            return base.Place(clicked, replace, face, clickPos, player, item);
        }

        public override bool Activate(Player player, ItemStack item)
        {
            return base.Activate(player, item);
        }

        public override bool Break(Player player, ItemStack item)
        {
            return base.Break(player, item);
        }
    }
}
