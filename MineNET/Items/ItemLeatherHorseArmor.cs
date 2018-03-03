using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemLeatherHorseArmor : Item
    {
        public ItemLeatherHorseArmor() : base(ItemFactory.LEATHER_HORSE_ARMOR)
        {

        }

        public override string Name
        {
            get
            {
                return "LeatherHorseArmor";
            }
        }
    }
}
