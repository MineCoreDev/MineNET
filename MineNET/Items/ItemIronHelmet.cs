using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemIronHelmet : ItemArmor
    {
        public ItemIronHelmet() : base(ItemFactory.IRON_HELMET)
        {

        }

        public override string Name
        {
            get
            {
                return "IronHelmet";
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
