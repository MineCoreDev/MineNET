using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemRecordFar : ItemRecordBase
    {
        public ItemRecordFar() : base(ItemFactory.RECORD_FAR)
        {

        }

        public override string Name
        {
            get
            {
                return "RecordFar";
            }
        }
    }
}
