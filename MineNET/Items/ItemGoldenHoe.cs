using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemGoldenHoe : ItemTool
    {
        public ItemGoldenHoe() : base(ItemFactory.GOLDEN_HOE)
        {

        }

        public override string Name
        {
            get
            {
                return "GoldenHoe";
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
