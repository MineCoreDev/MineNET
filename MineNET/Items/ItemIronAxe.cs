using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemIronAxe : ItemTool
    {
        public ItemIronAxe() : base(ItemFactory.IRON_AXE)
        {

        }

        public override string Name
        {
            get
            {
                return "IronAxe";
            }
        }

        public override bool IsAxe
        {
            get
            {
                return true;
            }
        }
    }
}
