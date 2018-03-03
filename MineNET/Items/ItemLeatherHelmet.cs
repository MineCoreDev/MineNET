using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemLeatherHelmet : ItemArmor
    {
        public ItemLeatherHelmet() : base(ItemFactory.LEATHER_HELMET)
        {

        }

        public override string Name
        {
            get
            {
                return "LeatherHelmet";
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
