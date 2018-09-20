using MineNET.Blocks;
using MineNET.Entities.Players;
using MineNET.Items;

namespace MineNET.Events.BlockEvents
{
    public class BlockBreakEventArgs : BlockEventArgs, ICancelable
    {
        public Player Player { get; set; }
        public ItemStack Item { get; set; }
        public ItemStack[] Drops { get; set; }

        public bool IsCancel { get; set; }

        public BlockBreakEventArgs(Player player, Block block, ItemStack item) : base(block)
        {
            this.Player = player;
            this.Item = item;
            this.Drops = block.GetDrops(item);
        }
    }
}
