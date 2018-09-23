using MineNET.Entities.Players;
using MineNET.Items;

namespace MineNET.Events.PlayerEvents
{
    public class PlayerBlockPickRequestEventArgs : PlayerEventArgs, ICancelable
    {
        public ItemStack Item { get; }

        public bool IsCancel { get; set; }

        public PlayerBlockPickRequestEventArgs(Player player, ItemStack item) : base(player)
        {
            this.Item = item;
        }
    }
}
