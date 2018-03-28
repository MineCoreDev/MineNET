using MineNET.Blocks;
using MineNET.Entities.Players;
using MineNET.Items;

namespace MineNET.Events.BlockEvents
{
    public class BlockPlaceEventArgs : BlockEventArgs, ICancellable
    {
        public Player Player { get; set; }
        public Block Replace { get; set; }
        public Block Clicked { get; set; }
        public Item Item { get; set; }

        public bool IsCancel { get; set; }

        public BlockPlaceEventArgs(Player player, Block place, Block replace, Block clicked, Item item) : base(place)
        {
            this.Player = player;
            this.Replace = replace;
            this.Clicked = clicked;
            this.Item = item;
        }
    }
}
