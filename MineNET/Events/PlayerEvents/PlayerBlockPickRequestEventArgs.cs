using MineNET.Entities.Players;
using MineNET.Items;

namespace MineNET.Events.PlayerEvents
{
    public class PlayerBlockPickRequestEventArgs : PlayerEventArgs, ICancellable
    {
        public Item Item { get; }

        public bool IsCancel { get; set; }

        public PlayerBlockPickRequestEventArgs(Player player, Item item) : base(player)
        {
            this.Item = item;
        }
    }
}
