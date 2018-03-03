using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemPaper : Item
    {
        public ItemPaper() : base(ItemFactory.PAPER)
        {

        }

        public override string Name
        {
            get
            {
                return "Paper";
            }
        }
    }
}
