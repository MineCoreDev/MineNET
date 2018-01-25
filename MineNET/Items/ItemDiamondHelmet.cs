using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemDiamondHelmet : ItemArmor
    {
        public ItemDiamondHelmet() :  base(ItemFactory.DIAMOND_HELMET)
        {

        }

        public override string Name
        {
            get
            {
                return "DiamondHelmet";
            }
        }

        public override bool IsHemlet
        {
            get
            {
                return true;
            }
        }
    }
}
