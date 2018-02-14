using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemIronBoots : ItemArmor
    {
        public ItemIronBoots() : base(ItemFactory.IRON_BOOTS)
        {

        }

        public override string Name
        {
            get
            {
                return "IronBoots";
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
