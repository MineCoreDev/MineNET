using MineNET.Blocks;
using MineNET.Entities.Players;
using MineNET.Items;

namespace MineNET.Events.PlayerEvents
{
    public class PlayerBlockPickRequestEventArgs : PlayerEventArgs, ICancelable
    {
        public Block Block { get; }
        public Item Item { get; set; }
        public bool RequestBlockData { get; }

        public bool IsCancel { get; set; }

        public PlayerBlockPickRequestEventArgs(Player player, Block block, Item item, bool requestBlockData) : base(player)
        {
            this.Block = block;
            this.Item = item;
            this.RequestBlockData = requestBlockData;
        }
    }
}
