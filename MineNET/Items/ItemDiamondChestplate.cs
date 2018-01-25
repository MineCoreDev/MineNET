using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemDiamondChestplate : ItemArmor
    {
        public ItemDiamondChestplate() : base(ItemFactory.DIAMOND_CHESTPLATE)
        {

        }

        public override string Name
        {
            get
            {
                return "DiamondChestplate";
            }
        }

        public override bool IsChestplate
        {
            get
            {
                return true;
            }
        }
    }
}
