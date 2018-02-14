using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemBeetroot : Item
    {
        public ItemBeetroot() : base(ItemFactory.BEETROOT)
        {

        }

        public override string Name
        {
            get
            {
                return "Beetroot";
            }
        }
    }
}
