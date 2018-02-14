using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemTntMinecart :  Item
    {
        public ItemTntMinecart() : base(ItemFactory.TNT_MINECART)
        {

        }

        public override string Name
        {
            get
            {
                return "TntMinecart";
            }
        }
    }
}
