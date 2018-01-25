using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemDiamondHorseArmor : Item
    {
        public ItemDiamondHorseArmor() : base(ItemFactory.DIAMOND_HORSE_ARMOR)
        {

        }

        public override string Name
        {
            get
            {
                return "DiamondHorseArmor";
            }
        }
    }
}
