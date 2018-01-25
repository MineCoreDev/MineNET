using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemItemFrame : Item
    {
        public ItemItemFrame() : base(ItemFactory.ITEM_FRAME)
        {

        }

        public override string Name
        {
            get
            {
                return "ItemFrame";
            }
        }
    }
}
