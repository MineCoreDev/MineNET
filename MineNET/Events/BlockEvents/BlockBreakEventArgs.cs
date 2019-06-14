using MineNET.Blocks;
using MineNET.Entities.Players;
using MineNET.Items;

namespace MineNET.Events.BlockEvents
{
    public class BlockBreakEventArgs : BlockEventArgs, ICancelable
    {
        public Player Player { get; set; }
        public Item Item { get; set; }
        public Item[] Drops { get; set; }

        public bool IsCancel { get; set; }

        public BlockBreakEventArgs(Player player, Block block, Item item) : base(block)
        {
            this.Player = player;
            this.Item = item;
            this.Drops = block.GetDrops(item);
        }
    }
}
