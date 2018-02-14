using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemDiamond : Item
    {
        public ItemDiamond() : base(ItemFactory.DIAMOND)
        {

        }

        public override string Name
        {
            get
            {
                return "Diamond";
            }
        }
    }
}
