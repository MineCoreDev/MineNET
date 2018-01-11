using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemStoneSword : ItemTool
    {
        public ItemStoneSword() : base(ItemFactory.STONE_SWORD)
        {

        }

        public override string Name
        {
            get
            {
                return "StoneSword";
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
