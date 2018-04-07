using System;
using MineNET.Entities.Data;

namespace MineNET.Items
{
    public class ItemChicken : ItemFood
    {
        public ItemChicken() : base(ItemFactory.CHICKEN)
        {

        }

        public override string Name
        {
            get
            {
                return "Chicken";
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
                if (new Random().Next(10) <= 3)
                {
                    return new Effect[]
                    {
                        Effect.GetEffect(Effect.NAUSEA, 600)
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
