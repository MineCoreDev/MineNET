using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemWoodenShovel : ItemTool
    {
        public ItemWoodenShovel() : base(ItemFactory.WOODEN_SHOVEL)
        {

        }

        public override string Name
        {
            get
            {
                return "WoodenShovel";
            }
        }

        public override bool IsShovel
        {
            get
            {
                return true;
            }
        }
    }
}
