using MineNET.Entities.Players;
using MineNET.Items;

namespace MineNET.Events.PlayerEvents
{
    public class PlayerItemConsumeableEventArgs : PlayerEventArgs, ICancellable
    {
        public Item Item { get; }

        public bool IsCancel { get; set; }

        public PlayerItemConsumeableEventArgs(Player player, Item item) : base(player)
        {
            this.Item = item;
        }
    }
}
