using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemBed : Item
    {
        public ItemBed() : base(ItemFactory.BED)
        {

        }

        public override string Name
        {
            get
            {
                return "Bed";
            }
        }
    }
}
