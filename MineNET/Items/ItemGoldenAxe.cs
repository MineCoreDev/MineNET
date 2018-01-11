using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemGoldenAxe : ItemTool
    {
        public ItemGoldenAxe() : base(ItemFactory.GOLDEN_AXE)
        {

        }

        public override string Name
        {
            get
            {
                return "GoldenAxe";
            }
        }

        public override bool IsAxe
        {
            get
            {
                return true;
            }
        }
    }
}
