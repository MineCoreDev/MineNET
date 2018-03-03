using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemGoldIngot : Item
    {
        public ItemGoldIngot() : base(ItemFactory.GOLD_INGOT)
        {

        }

        public override string Name
        {
            get
            {
                return "GoldIngot";
            }
        }
    }
}
