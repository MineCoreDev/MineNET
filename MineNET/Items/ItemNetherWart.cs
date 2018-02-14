using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemNetherWart : Item
    {
        public ItemNetherWart() : base(ItemFactory.NETHER_WART)
        {

        }

        public override string Name
        {
            get
            {
                return "NetherWart";
            }
        }
    }
}
