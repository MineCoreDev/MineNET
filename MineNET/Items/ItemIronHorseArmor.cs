using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemIronHorseArmor : Item
    {
        public ItemIronHorseArmor() : base(ItemFactory.IRON_HORSE_ARMOR)
        {

        }

        public override string Name
        {
            get
            {
                return "IronHorseArmor";
            }
        }
    }
}
