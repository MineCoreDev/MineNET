using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemTotem : Item
    {
        public ItemTotem() : base(ItemFactory.TOTEM)
        {

        }

        public override string Name
        {
            get
            {
                return "Totem";
            }
        }
    }
}
