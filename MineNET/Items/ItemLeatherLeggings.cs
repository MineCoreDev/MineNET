using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemLeatherLeggings : ItemArmor
    {
        public ItemLeatherLeggings() : base(ItemFactory.LEATHER_LEGGINGS)
        {

        }

        public override string Name
        {
            get
            {
                return "LeatherLeggings";
            }
        }

        public override bool IsLeggings
        {
            get
            {
                return true;
            }
        }
    }
}
