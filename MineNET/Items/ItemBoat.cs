using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemBoat : Item
    {
        public ItemBoat() : base(ItemFactory.BOAT)
        {

        }

        public override string Name
        {
            get
            {
                return "Boat";
            }
        }
    }
}
