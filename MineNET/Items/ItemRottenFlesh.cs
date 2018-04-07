using System;
using MineNET.Entities.Data;

namespace MineNET.Items
{
    public class ItemRottenFlesh : ItemFood
    {
        public ItemRottenFlesh() : base(ItemFactory.ROTTEN_FLESH)
        {

        }

        public override string Name
        {
            get
            {
                return "RottemFlesh";
            }
        }

        public override int FoodRestore
        {
            get
            {
                return 4;
            }
        }

        public override float SaturationRestore
        {
            get
            {
                return 0.8f;
            }
        }

        public override Effect[] AdditionalEffects
        {
            get
            {
                if (new Random().Next(10) <= 8)
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
