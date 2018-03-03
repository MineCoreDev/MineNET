using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemPoisonousPotato : Item
    {
        public ItemPoisonousPotato() : base(ItemFactory.POISONOUS_POTATO)
        {

        }

        public override string Name
        {
            get
            {
                return "PoisonousPotato";
            }
        }
    }
}
