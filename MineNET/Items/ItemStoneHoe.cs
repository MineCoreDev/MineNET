using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemStoneHoe : ItemTool
    {
        public ItemStoneHoe() : base(ItemFactory.STONE_HOE)
        {

        }

        public override string Name
        {
            get
            {
                return "StoneHoe";
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
