using MineNET.Entities.Players;
using MineNET.Items.Data;

namespace MineNET.Events.PlayerEvents
{
    public class PlayerItemConsumeableEventArgs : PlayerEventArgs, ICancellable
    {
        public IConsumeable Consumeable { get; }

        public bool IsCancel { get; set; }

        public PlayerItemConsumeableEventArgs(Player player, IConsumeable consumeable) : base(player)
        {
            this.Consumeable = consumeable;
        }
    }
}
