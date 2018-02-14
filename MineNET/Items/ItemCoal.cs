using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemCoal : Item
    {
        public ItemCoal() : base(ItemFactory.COAL)
        {

        }

        public override string Name
        {
            get
            {
                return "Coal";
            }
        }
    }
}
