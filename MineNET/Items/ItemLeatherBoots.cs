using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemLeatherBoots : ItemArmor
    {
        public ItemLeatherBoots() : base(ItemFactory.LEATHER_BOOTS)
        {

        }

        public override string Name
        {
            get
            {
                return "LeatherBoots";
            }
        }

        public override bool IsBoots
        {
            get
            {
                return true;
            }
        }
    }
}
