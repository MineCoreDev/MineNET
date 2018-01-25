using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    class ItemRecordBlocks : ItemRecordBase
    {
        public ItemRecordBlocks() : base(ItemFactory.RECORD_BLOCKS)
        {

        }

        public override string Name
        {
            get
            {
                return "RecordBlocks";
            }
        }
    }
}
