using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemIronLeggings : ItemArmor
    {
        public ItemIronLeggings() : base(ItemFactory.IRON_LEGGINGS)
        {

        }

        public override string Name
        {
            get
            {
                return "IronLeggings";
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
