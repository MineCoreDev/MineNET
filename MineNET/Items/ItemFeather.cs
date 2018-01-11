using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemFeather : ItemTool
    {
        public ItemFeather() : base(ItemFactory.FEATHER)
        {

        }

        public override string Name
        {
            get
            {
                return "Feather";
            }
        }
    }
}
