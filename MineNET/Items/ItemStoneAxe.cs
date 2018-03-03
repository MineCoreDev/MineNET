using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemStoneAxe : ItemTool
    {
        public ItemStoneAxe() : base(ItemFactory.STONE_AXE)
        {

        }

        public override string Name
        {
            get
            {
                return "StoneAxe";
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
