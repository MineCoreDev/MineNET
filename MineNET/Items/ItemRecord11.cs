using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemRecord11 : ItemRecordBase
    {
        public ItemRecord11() : base(ItemFactory.RECORD_11)
        {

        }

        public override string Name
        {
            get
            {
                return "Record11";
            }
        }
    }
}
