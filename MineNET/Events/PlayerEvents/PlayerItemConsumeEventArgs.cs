using MineNET.Entities.Players;
using MineNET.Items;

namespace MineNET.Events.PlayerEvents
{
    public class PlayerItemConsumeEventArgs : PlayerEventArgs, ICancelable
    {
        public Item Item { get; }
        public IConsumeable Consume { get; }

        public bool IsCancel { get; set; }

        public PlayerItemConsumeEventArgs(Player player, Item item, IConsumeable consume) : base(player)
        {
            this.Item = item;
            this.Consume = consume;
        }
    }
}
