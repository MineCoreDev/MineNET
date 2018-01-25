using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemGoldenLeggings : ItemArmor
    {
        public ItemGoldenLeggings() : base(ItemFactory.GOLDEN_LEGGINGS)
        {

        }

        public override string Name
        {
            get
            {
                return "GoldenLeggings";
            }
        }

        public override bool IsLeggings
        {
            get
            {
                return true;
            }
        }
    }
}
