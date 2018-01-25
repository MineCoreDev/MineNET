using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemGoldenHelmet : ItemArmor
    {
        public ItemGoldenHelmet() : base(ItemFactory.GOLDEN_HELMET)
        {

        }

        public override string Name
        {
            get
            {
                return "GoldenHelmet";
            }
        }

        public override bool IsHemlet
        {
            get
            {
                return true;
            }
        }
    }
}
