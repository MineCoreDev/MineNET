using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemGoldenApple : ItemFood
    {
        public ItemGoldenApple() : base(ItemFactory.GOLDEN_APPLE)
        {

        }

        public override string Name
        {
            get
            {
                return "GoldenApple";
            }
        }
    }
}
