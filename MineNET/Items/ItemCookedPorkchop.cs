using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemCookedPorkchop : Item
    {
        public ItemCookedPorkchop() : base(ItemFactory.COOKED_PORKCHOP)
        {

        }

        public override string Name
        {
            get
            {
                return "CookedPorkchop";
            }
        }
    }
}
