using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemDiamondPickaxe : ItemTool
    {
        public ItemDiamondPickaxe() : base(ItemFactory.DIAMOND_PICKAXE)
        {

        }

        public override string Name
        {
            get
            {
                return "DiamondPickaxe";
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
