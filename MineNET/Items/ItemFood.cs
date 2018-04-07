using MineNET.Entities.Data;
using MineNET.Entities.Players;
using MineNET.Events.PlayerEvents;
using MineNET.Items.Data;

namespace MineNET.Items
{
    public abstract class ItemFood : Item, IConsumeable
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
                player.Inventory.SendMainHand(player);
                return;
            }
            player.AddHunger(this.FoodRestore);
            player.AddSaturation(this.SaturationRestore);
            Effect[] effects = this.AdditionalEffects;
            for (int i = 0; i < effects.Length; ++i)
            {
                player.AddEffect(effects[i]);
            }

            this.Count--;
            player.Inventory.MainHandItem = this;
        }

        public virtual Effect[] AdditionalEffects
        {
            get
            {
                return new Effect[0];
            }
        }
    }
}
