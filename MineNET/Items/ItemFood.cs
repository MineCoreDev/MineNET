using MineNET.Entities.Players;
using MineNET.Events.PlayerEvents;
using MineNET.Items.Data;
using MineNET.Utils;

namespace MineNET.Items
{
    public abstract class ItemFood : Item, Consumeable
    {
        public ItemFood(int id) : base(id)
        {

        }

        public override bool CanBeConsumed
        {
            get
            {
                return true;
            }
        }

        public abstract int FoodRestore { get; }

        public abstract float SaturationRestore { get; }

        public void OnConsume(Player player)
        {
            PlayerEatFoodEvent playerEatFoodEvent = new PlayerEatFoodEvent(player, this);
            PlayerEvents.OnPlayerEatFood(playerEatFoodEvent);
            if (playerEatFoodEvent.IsCancel)
            {
                Logger.Info(4);
                player.Inventory.SendMainHand(player);
                return;
            }
            Logger.Info(this.FoodRestore);
            Logger.Info(this.SaturationRestore);
            player.AddHunger(this.FoodRestore);
            player.AddSaturation(this.SaturationRestore);

            this.Count--;
            player.Inventory.MainHandItem = this;
        }
    }
}
