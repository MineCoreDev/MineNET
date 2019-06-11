using MineNET.Entities.Players;
using MineNET.Items;

namespace MineNET.Events.PlayerEvents
{
    public class PlayerItemConsumeEventArgs : PlayerEventArgs, ICancelable
    {
        public ItemStack ItemStack { get; }
        public IConsumeable Consume { get; }

        public bool IsCancel { get; set; }

        public PlayerItemConsumeEventArgs(Player player, ItemStack stack, IConsumeable consume) : base(player)
        {
            this.ItemStack = stack;
            this.Consume = consume;
        }
    }
}
