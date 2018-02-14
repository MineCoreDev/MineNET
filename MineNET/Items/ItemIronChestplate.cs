using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemIronChestplate : ItemArmor
    {
        public ItemIronChestplate() : base(ItemFactory.IRON_CHESTPLATE)
        {

        }

        public override string Name
        {
            get
            {
                return "IronChestplate";
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
