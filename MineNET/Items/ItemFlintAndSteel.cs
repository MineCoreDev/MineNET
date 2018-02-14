using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemFlintAndSteel : ItemTool
    {
        public ItemFlintAndSteel() : base(ItemFactory.FLINT_AND_STEEL)
        {

        }

        public override string Name
        {
            get
            {
                return "FlintAndSteel";
            }
        }

        public override bool IsFlintAndSteel
        {
            get
            {
                return true;
            }
        }
    }
}
