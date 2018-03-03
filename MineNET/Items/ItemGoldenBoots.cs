using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemGoldenBoots : ItemArmor
    {
        public ItemGoldenBoots() : base(ItemFactory.GOLDEN_BOOTS)
        {

        }

        public override string Name
        {
            get
            {
                return "GoldenBoots";
            }
        }

        public override bool IsBoots
        {
            get
            {
                return true;
            }
        }
    }
}
