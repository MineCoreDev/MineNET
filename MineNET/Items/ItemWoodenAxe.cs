using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemWoodenAxe : ItemTool
    {
        public ItemWoodenAxe() : base(ItemFactory.WOODEN_AXE)
        {

        }

        public override string Name
        {
            get
            {
                return "WoodenAxe";
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
