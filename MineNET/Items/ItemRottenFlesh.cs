using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemRottenFlesh : Item
    {
        public ItemRottenFlesh() : base(ItemFactory.ROTTEN_FLESH)
        {

        }

        public override string Name
        {
            get
            {
                return "RottemFlesh";
            }
        }
    }
}
