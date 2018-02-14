using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemSalmon : ItemFood
    {
        public ItemSalmon() : base(ItemFactory.SALMON)
        {

        }

        public override string Name
        {
            get
            {
                return "Salmon";
            }
        }
    }
}
