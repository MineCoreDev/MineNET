using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemMelon : Item
    {
        public ItemMelon() : base(ItemFactory.MELON)
        {

        }

        public override string Name
        {
            get
            {
                return "Melon";
            }
        }
    }
}
