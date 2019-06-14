using MineNET.Entities.Players;
using MineNET.Items;

namespace MineNET.Events.PlayerEvents
{
    public class PlayerEatFoodEventArgs : PlayerEventArgs, ICancelable
    {
        public ItemFood Food { get; }
        public Item Residue { get; set; }

        public bool IsCancel { get; set; }

        public PlayerEatFoodEventArgs(Player player, ItemFood food, Item residue) : base(player)
        {
            this.Food = food;
            this.Residue = residue;
        }
    }
}
