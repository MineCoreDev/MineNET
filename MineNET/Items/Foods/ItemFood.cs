using MineNET.Blocks;
using MineNET.Entities;
using MineNET.Entities.Players;
using MineNET.Events.PlayerEvents;

namespace MineNET.Items
{
    public abstract class ItemFood : Item, IConsumeable
    {
        public abstract int FoodRestore { get; }

        public abstract float SaturationRestore { get; }

        public virtual void OnConsume(Player player)
        {
            Item food = this.Clone();
            PlayerEatFoodEventArgs args = new PlayerEatFoodEventArgs(player, (ItemFood) food, this.Residue);
            Server.Instance.Event.Player.OnPlayerEatFood(this, args);
            if (args.IsCancel)
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

            if (player.IsCreative)
            {
                return;
            }

            food.Count--;
            if (food.Count < 1)
            {
                food = Item.Get(BlockIDs.AIR);
            }
            player.Inventory.SetItem(player.Inventory.MainHandSlot, food);

            Item residue = args.Residue;
            if (residue.ID == BlockIDs.AIR)
            {
                return;
            }

            if (food.ID == BlockIDs.AIR)
            {
                player.Inventory.SetItem(player.Inventory.MainHandSlot, residue);
            }
            else
            {
                player.Inventory.AddItem(residue);
            }
        }

        public virtual Effect[] AdditionalEffects => new Effect[0];

        public virtual Item Residue => Item.Get(BlockIDs.AIR);
    }
}
