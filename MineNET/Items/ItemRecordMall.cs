using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    class ItemRecordMall : ItemRecordBase
    {
        public ItemRecordMall() : base(ItemFactory.RECORD_MALL)
        {

        }

        public override string Name
        {
            get
            {
                return "RecordMall";
            }
        }
    }
}
