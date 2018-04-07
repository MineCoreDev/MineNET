using MineNET.Entities.Data;

namespace MineNET.Items
{
    public class ItemSpiderEye : ItemFood
    {
        public ItemSpiderEye() : base(ItemFactory.SPIDER_EYE)
        {

        }

        public override string Name
        {
            get
            {
                return "SpiderEye";
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
                return new Effect[]
                {
                     Effect.GetEffect(Effect.POISON, 80)
                };
            }
        }
    }
}
