using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemBook : Item
    {
        public ItemBook() : base(ItemFactory.BOOK)
        {

        }

        public override string Name
        {
            get
            {
                return "Book";
            }
        }
    }
}
