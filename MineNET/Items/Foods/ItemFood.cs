using MineNET.Blocks;
using MineNET.Entities;
using MineNET.Entities.Players;
using MineNET.Events.PlayerEvents;

namespace MineNET.Items
{
    public abstract class ItemFood : Item, IConsumeable
    {
        public override bool CanBeConsumed { get; } = true;

        public abstract int FoodRestore { get; }

        public abstract float SaturationRestore { get; }

        public virtual void OnConsume(Player player, ItemStack food)
        {
            PlayerEatFoodEventArgs args = new PlayerEatFoodEventArgs(player, food, this, this.Residue);
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
                food = new ItemStack(BlockIDs.AIR);
            }
            player.Inventory.SetItem(player.Inventory.MainHandSlot, food);

            ItemStack stack = new ItemStack(args.Residue);
            if (stack.ID == BlockIDs.AIR)
            {
                return;
            }

            if (food.ID == BlockIDs.AIR)
            {
                player.Inventory.SetItem(player.Inventory.MainHandSlot, stack);
            }
            else
            {
                player.Inventory.AddItem(stack);
            }
        }

        public virtual Effect[] AdditionalEffects => new Effect[0];

        public virtual Item Residue => Item.Get(BlockIDs.AIR);
    }
}
