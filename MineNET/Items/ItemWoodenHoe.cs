using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemWoodenHoe : ItemTool
    {
        public ItemWoodenHoe() : base(ItemFactory.WOODEN_HOE)
        {

        }

        public override string Name
        {
            get
            {
                return "WoodenHoe";
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
