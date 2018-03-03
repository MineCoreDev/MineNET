using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemSugar : Item
    {
        public ItemSugar() : base(ItemFactory.SUGAR)
        {

        }

        public override string Name
        {
            get
            {
                return "Suger";
            }
        }
    }
}
