using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemBanner : Item
    {
        public ItemBanner() : base(ItemFactory.BANNER)
        {

        }

        public override string Name
        {
            get
            {
                return "Banner";
            }
        }
    }
}
