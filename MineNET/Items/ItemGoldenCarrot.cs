using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemGoldenCarrot : Item
    {
        public ItemGoldenCarrot() : base(ItemFactory.GOLDEN_CARROT)
        {

        }

        public override string Name
        {
            get
            {
                return "GoldenCarrot";
            }
        }
    }
}
