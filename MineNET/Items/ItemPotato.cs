using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemPotato : Item
    {
        public ItemPotato() : base(ItemFactory.POTATO)
        {

        }

        public override string Name
        {
            get
            {
                return "Potato";
            }
        }
    }
}
