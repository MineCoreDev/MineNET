using MineNET.Entities.Players;
using MineNET.Items;

namespace MineNET.Events.PlayerEvents
{
    public class PlayerEatFoodEvent : PlayerEventArgs, ICancellable
    {
        public ItemFood Food { get; }

        public bool IsCancel { get; set; }

        public PlayerEatFoodEvent(Player player, ItemFood food) : base(player)
        {
            this.Food = food;
        }
    }
}
