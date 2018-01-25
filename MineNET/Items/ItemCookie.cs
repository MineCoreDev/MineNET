using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemCookie : Item
    {
        public ItemCookie() : base(ItemFactory.COOKIE)
        {

        }

        public override string Name
        {
            get
            {
                return "Cookie";
            }
        }
    }
}
