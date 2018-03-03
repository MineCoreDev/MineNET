using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemDiamondLeggings : ItemArmor
    {
        public ItemDiamondLeggings() : base(ItemFactory.DIAMOND_LEGGINGS)
        {

        }

        public override string Name
        {
            get
            {
                return "DiamondLeggings";
            }
        }

        public override bool IsLeggings
        {
            get
            {
                return true;
            }
        }
    }
}
