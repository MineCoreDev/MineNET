using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemLeatherChestplate : ItemArmor
    {
        public ItemLeatherChestplate() : base(ItemFactory.LEATHER_CHESTPLATE)
        {

        }

        public override string Name
        {
            get
            {
                return "LeatherChestplate";
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
