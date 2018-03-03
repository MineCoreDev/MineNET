using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemHopper : Item
    {
        public ItemHopper() : base(ItemFactory.HOPPER)
        {

        }

        public override string Name
        {
            get
            {
                return "Hopper";
            }
        }
    }
}
