using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemRecordWait : ItemRecordBase
    {
        public ItemRecordWait() : base(ItemFactory.RECORD_WAIT)
        {

        }

        public override string Name
        {
            get
            {
                return "RecordWait";
            }
        }
    }
}
