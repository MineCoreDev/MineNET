using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemString : Item
    {
        public ItemString() : base(ItemFactory.STRING)
        {

        }

        public override string Name
        {
            get
            {
                return "String";
            }
        }
    }
}
