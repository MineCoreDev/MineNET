using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemEmerald : Item
    {
        public ItemEmerald() : base(ItemFactory.EMERALD)
        {

        }

        public override string Name
        {
            get
            {
                return "Emerald";
            }
        }
    }
}
