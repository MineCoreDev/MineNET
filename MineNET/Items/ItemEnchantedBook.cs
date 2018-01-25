using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemEnchantedBook : Item
    {
        public ItemEnchantedBook() : base(ItemFactory.ENCHANTED_BOOK)
        {

        }

        public override string Name
        {
            get
            {
                return "EnchantedBook";
            }
        }
    }
}
