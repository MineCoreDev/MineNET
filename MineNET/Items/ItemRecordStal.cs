using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    class ItemRecordStal : ItemRecordBase
    {
        public ItemRecordStal() : base(ItemFactory.RECORD_STAL)
        {

        }

        public override string Name
        {
            get
            {
                return "RecordStal";
            }
        }
    }
}
