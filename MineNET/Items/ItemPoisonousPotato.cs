using System;
using MineNET.Entities.Data;

namespace MineNET.Items
{
    public class ItemPoisonousPotato : ItemFood
    {
        public ItemPoisonousPotato() : base(ItemFactory.POISONOUS_POTATO)
        {

        }

        public override string Name
        {
            get
            {
                return "PoisonousPotato";
            }
        }

        public override int FoodRestore
        {
            get
            {
                return 2;
            }
        }

        public override float SaturationRestore
        {
            get
            {
                return 1.2f;
            }
        }

        public override Effect[] AdditionalEffects
        {
            get
            {
                if (new Random().Next(10) <= 6)
                {
                    return new Effect[]
                    {
                        Effect.GetEffect(Effect.POISON, 80)
                    };
                }
                else
                {
                    return new Effect[0];
                }
            }
        }
    }
}
