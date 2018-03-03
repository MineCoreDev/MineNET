using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemNameTag : Item
    {
        public ItemNameTag() : base(ItemFactory.NAME_TAG)
        {

        }

        public override string Name
        {
            get
            {
                return "NameTag";
            }
        }
    }
}
