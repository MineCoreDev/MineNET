using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    class IronPickaxe : ItemTool
    {
        public IronPickaxe() : base(ItemFactory.IRON_PICKAXE)
        {

        }

        public override string Name
        {
            get
            {
                return "IronPickaxe";
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
