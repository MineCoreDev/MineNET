using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemBow : ItemTool
    {
        public ItemBow() : base(ItemFactory.BOW)
        {

        }

        public override string Name
        {
            get
            {
                return "Bow";
            }
        }
    }
}
