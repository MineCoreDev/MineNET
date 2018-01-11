using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemGoldenPickaxe : ItemTool
    {
        public ItemGoldenPickaxe() : base(ItemFactory.GOLDEN_PICKAXE)
        {

        }

        public override string Name
        {
            get
            {
                return "GoldenPickaxe";
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
