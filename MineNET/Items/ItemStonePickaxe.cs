using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemStonePickaxe : ItemTool
    {
        public ItemStonePickaxe() : base(ItemFactory.STONE_PICKAXE)
        {

        }

        public override string Name
        {
            get
            {
                return "StonePickaxe";
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
