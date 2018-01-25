using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemArmorStand : Item
    {
        public ItemArmorStand() : base(ItemFactory.ARMOR_STAND)
        {

        }

        public override string Name
        {
            get
            {
                return "ArmorStand";
            }
        }
    }
}
