using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemArmor : Item
    {
        public ItemArmor(int id): base(id)
        {

        }

        public override bool IsArmor
        {
            get
            {
                return true;
            }
        }
    }
}
