using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemIronShovel : ItemTool
    {
        public ItemIronShovel() : base(ItemFactory.IRON_SHOVEL)
        {

        }

        public override string Name
        {
            get
            {
                return "IronShovel";
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
