using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemDiamondHoe : ItemTool
    {
        public ItemDiamondHoe() : base(ItemFactory.DIAMOND_HOE)
        {

        }

        public override string Name
        {
            get
            {
                return "DiamondHoe";
            }
        }

        public override bool IsHoe
        {
            get
            {
                return true;
            }
        }
    }
}
