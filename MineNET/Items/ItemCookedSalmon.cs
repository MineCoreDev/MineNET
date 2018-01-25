using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    class ItemCookedSalmon : ItemFood
    {
        public ItemCookedSalmon() : base(ItemFactory.COOKED_SALMON)
        {

        }

        public override string Name
        {
            get
            {
                return "CookedSalmon";
            }
        }
    }
}
