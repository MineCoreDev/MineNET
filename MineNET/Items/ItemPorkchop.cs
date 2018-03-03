using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemPorkchop : Item
    {
        public ItemPorkchop() : base(ItemFactory.PORKCHOP)
        {

        }

        public override string Name
        {
            get
            {
                return "Porkchop";
            }
        }
    }
}
