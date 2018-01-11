using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemIronIngot : Item
    {
        public ItemIronIngot() : base(ItemFactory.IRON_INGOT)
        {

        }

        public override string Name
        {
            get
            {
                return "IronIngot";
            }
        }
    }
}
