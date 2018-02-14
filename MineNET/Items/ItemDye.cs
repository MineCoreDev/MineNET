using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemDye : Item
    {
        public ItemDye() : base(ItemFactory.DYE)
        {

        }

        public override string Name
        {
            get
            {
                return "Dye";
            }
        }
    }
}
