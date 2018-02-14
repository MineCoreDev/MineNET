using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemDiamondBoots : ItemArmor
    {
        public ItemDiamondBoots() : base(ItemFactory.DIAMOND_BOOTS)
        {

        }

        public override string Name
        {
            get
            {
                return "DiamondBoots";
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
