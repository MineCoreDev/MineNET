using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemIronHoe : ItemTool
    {
        public ItemIronHoe() : base(ItemFactory.IRON_HOE)
        {

        }

        public override string Name
        {
            get
            {
                return "IronHoe";
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
