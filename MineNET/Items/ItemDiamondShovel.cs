using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemDiamondShovel : ItemTool
    {
        public ItemDiamondShovel() : base(ItemFactory.DIAMOND_SHOVEL)
        {

        }

        public override string Name
        {
            get
            {
                return "DiamondShovel";
            }
        }

        public override bool IsShovel
        {
            get
            {
                return true;
            }
        }
    }
}
