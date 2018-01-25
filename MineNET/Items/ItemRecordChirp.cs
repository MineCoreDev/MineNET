using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    class ItemRecordChirp : ItemRecordBase
    {
        public ItemRecordChirp() : base(ItemFactory.RECORD_CHIRP)
        {

        }

        public override string Name
        {
            get
            {
                return "RecordChirp";
            }
        }
    }
}
