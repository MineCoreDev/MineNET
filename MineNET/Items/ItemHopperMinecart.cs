using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemHopperMinecart : Item
    {
        public ItemHopperMinecart() : base(ItemFactory.HOPPER_MINECART)
        {

        }

        public override string Name
        {
            get
            {
                return "HopperMinecart";
            }
        }
    }
}
