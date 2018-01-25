using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    class ItemRecordWard : ItemRecordBase
    {
        public ItemRecordWard() : base(ItemFactory.RECORD_WARD)
        {

        }

        public override string Name
        {
            get
            {
                return "RecordWard";
            }
        }
    }
}
