using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemStoneShovel : ItemTool
    {
        public ItemStoneShovel() : base(ItemFactory.STONE_SHOVEL)
        {

        }

        public override string Name
        {
            get
            {
                return "StoneShovel";
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
