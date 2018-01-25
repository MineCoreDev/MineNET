using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    class ItemRecordMellohi : ItemRecordBase
    {
        public ItemRecordMellohi() : base(ItemFactory.RECORD_MELLOHI)
        {

        }

        public override string Name
        {
            get
            {
                return "RecordMellohi";
            }
        }
    }
}
