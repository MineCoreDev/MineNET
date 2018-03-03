using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemMagmaCream : Item
    {
        public ItemMagmaCream() : base(ItemFactory.MAGMA_CREAM)
        {

        }

        public override string Name
        {
            get
            {
                return "MagmaCream";
            }
        }
    }
}
