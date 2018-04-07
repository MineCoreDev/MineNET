using MineNET.Entities.Data;

namespace MineNET.Items
{
    public class ItemGoldenApple : ItemFood
    {
        public ItemGoldenApple() : base(ItemFactory.GOLDEN_APPLE)
        {

        }

        public override string Name
        {
            get
            {
                return "GoldenApple";
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
                return 9.6f;
            }
        }

        public override Effect[] AdditionalEffects
        {
            get
            {
                return new Effect[]
                {
                    Effect.GetEffect(Effect.REGENERATION, 100, 2),
                    Effect.GetEffect(Effect.HEALTH_BOOST, 2400),
                };
            }
        }
    }
}
