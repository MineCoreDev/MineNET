using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemGoldenChestplate : ItemArmor
    {
        public ItemGoldenChestplate() : base(ItemFactory.GOLDEN_CHESTPLATE)
        {

        }

        public override string Name
        {
            get
            {
                return "GoldenChestplate";
            }
        }

        public override bool IsChestplate
        {
            get
            {
                return true;
            }
        }
    }
}
