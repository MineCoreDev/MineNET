using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemWoodenPickaxe : ItemTool
    {
        public ItemWoodenPickaxe() : base(ItemFactory.WOODEN_PICKAXE)
        {

        }

        public override string Name
        {
            get
            {
                return "WoodenPickaxe";
            }
        }

        public override bool IsPickaxe
        {
            get
            {
                return true;
            }
        }
    }
}
