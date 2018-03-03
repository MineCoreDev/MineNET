using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemGoldenHorseArmor : Item
    {
        public ItemGoldenHorseArmor() : base(ItemFactory.GOLDEN_HORSE_ARMOR)
        {

        }

        public override string Name
        {
            get
            {
                return "GoldenHorseArmor";
            }
        }
    }
}
