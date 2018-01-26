using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemBeetrootSoup : Item
    {
        public ItemBeetrootSoup() : base(ItemFactory.BEETROOT_SOUP)
        {

        }

        public override string Name
        {
            get
            {
                return "BeetrootSoup";
            }
        }
    }
}
