using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    class IronShovel : ItemTool
    {
        public IronShovel() : base(ItemFactory.IRON_SHOVEL)
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
