using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemIronSword : ItemTool
    {
        public ItemIronSword() : base(ItemFactory.IRON_SWORD)
        {

        }

        public override string Name
        {
            get
            {
                return "IronSword";
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
