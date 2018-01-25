using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    class ItemRecord13 : ItemRecordBase
    {
        public ItemRecord13() : base(ItemFactory.RECORD_13)
        {

        }

        public override string Name
        {
            get
            {
                return "Record13";
            }
        }
    }
}
