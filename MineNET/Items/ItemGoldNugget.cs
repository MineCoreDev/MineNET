using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    class ItemGoldNugget : Item
    {
        public ItemGoldNugget() : base(ItemFactory.GOLD_NUGGET)
        {

        }

        public override string Name
        {
            get
            {
                return "GoldNugget";
            }
        }
    }
}
