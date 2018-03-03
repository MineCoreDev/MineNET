using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemDiamondSword : ItemTool
    {
        public ItemDiamondSword() : base(ItemFactory.DIAMOND_SWORD)
        {

        }

        public override string Name
        {
            get
            {
                return "DiamondSword";
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
