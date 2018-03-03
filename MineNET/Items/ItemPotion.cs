using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemPotion : Item
    {
        public ItemPotion() : base(ItemFactory.POTION)
        {

        } 

        public override string Name
        {
            get
            {
                return "Potion";
            }
        }
    }
}
