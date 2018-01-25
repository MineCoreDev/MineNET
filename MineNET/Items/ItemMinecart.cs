using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemMinecart : Item
    {
        public ItemMinecart() : base(ItemFactory.MINECART)
        {

        }

        public override string Name
        {
            get
            {
                return "Minecart";
            }
        }
    }
}
