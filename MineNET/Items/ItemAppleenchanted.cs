using MineNET.Entities.Data;

namespace MineNET.Items
{
    public class ItemAppleenchanted : ItemFood
    {
        public ItemAppleenchanted() : base(ItemFactory.APPLEENCHANTED)
        {

        }

        public override string Name
        {
            get
            {
                return "Appleenchanted";
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
                    Effect.GetEffect(Effect.REGENERATION, 400, 2),
                    Effect.GetEffect(Effect.HEALTH_BOOST, 2400, 4),
                    Effect.GetEffect(Effect.RESISTANCE, 6000),
                    Effect.GetEffect(Effect.FIRE_RESISTANCE, 6000)
                };
            }
        }
    }
}
