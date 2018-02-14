using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemMutton : Item
    {
        public ItemMutton() : base(ItemFactory.MUTTON)
        {

        }

        public override string Name
        {
            get
            {
                return "Mutton";
            }
        }
    }
}
