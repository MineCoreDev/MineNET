using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemGoldenSword : ItemTool
    {
        public ItemGoldenSword() : base(ItemFactory.GOLDEN_SWORD)
        {

        }

        public override string Name
        {
            get
            {
                return "GoldenSword";
            }
        }

        public override bool IsSword
        {
            get
            {
                return true;
            }
        }
    }
}
