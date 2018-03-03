using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemDiamondAxe : ItemTool
    {
        public ItemDiamondAxe() : base(ItemFactory.DIAMOND_AXE)
        {

        }

        public override string Name
        {
            get
            {
                return "DiamondAxe";
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
