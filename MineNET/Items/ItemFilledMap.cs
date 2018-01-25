using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemFilledMap : Item
    {
        public ItemFilledMap() : base(ItemFactory.FILLED_MAP)
        {

        }

        public override string Name
        {
            get
            {
                return "FilledMap";
            }
        }
    }
}
