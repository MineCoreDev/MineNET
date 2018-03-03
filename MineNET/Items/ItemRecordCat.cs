using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemRecordCat : ItemRecordBase
    {
        public ItemRecordCat() : base(ItemFactory.RECORD_CAT)
        {

        }

        public override string Name
        {
            get
            {
                return "RecordCat";
            }
        }
    }
}
