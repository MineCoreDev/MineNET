using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemCookedBeef : ItemFood
    {
        public ItemCookedBeef() : base(ItemFactory.COOKED_BEEF)
        {

        }

        public override string Name
        {
            get
            {
                return "CookedBeef";
            }
        }
    }
}
