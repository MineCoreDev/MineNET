using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemEndCrystal : Item
    {
        public ItemEndCrystal() : base(ItemFactory.END_CRYSTAL)
        {

        }

        public override string Name
        {
            get
            {
                return "EndCrystal";
            }
        }
    }
}
