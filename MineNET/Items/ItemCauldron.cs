using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemCauldron : Item
    {
        public ItemCauldron() : base(ItemFactory.CAULDRON)
        {

        }

        public override string Name
        {
            get
            {
                return "Cauldron";
            }
        }
    }
}
