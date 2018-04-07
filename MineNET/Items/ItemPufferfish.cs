using MineNET.Entities.Data;

namespace MineNET.Items
{
    public class ItemPufferfish : ItemFood
    {
        public ItemPufferfish() : base(ItemFactory.PUFFERFISH)
        {

        }

        public override string Name
        {
            get
            {
                return "Pufferfish";
            }
        }

        public override int FoodRestore
        {
            get
            {
                return 1;
            }
        }

        public override float SaturationRestore
        {
            get
            {
                return 0.2f;
            }
        }

        public override Effect[] AdditionalEffects
        {
            get
            {
                return new Effect[]
                {
                    Effect.GetEffect(Effect.HUNGER, 300, 3),
                    Effect.GetEffect(Effect.NAUSEA, 300, 2),
                    Effect.GetEffect(Effect.POISON, 1200, 4)
                };
            }
        }
    }
}
