using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemClock : Item
    {
        public ItemClock() : base(ItemFactory.CLOCK)
        {

        }

        public override string Name
        {
            get
            {
                return "Clock";
            }
        }
    }
}
