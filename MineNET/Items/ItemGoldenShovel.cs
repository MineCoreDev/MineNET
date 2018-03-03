using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemGoldenShovel : ItemTool
    {
        public ItemGoldenShovel() : base(ItemFactory.GOLDEN_SHOVEL)
        {

        }

        public override string Name
        {
            get
            {
                return "GoldenShovel";
            }
        }

        public override bool IsShovel
        {
            get
            {
                return true;
            }
        }
    }
}
