using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    class ItemRecordStrad : ItemRecordBase
    {
        public ItemRecordStrad() : base(ItemFactory.RECORD_STRAD)
        {

        }

        public override string Name
        {
            get
            {
                return "RecordStrad";
            }
        }
    }
}
