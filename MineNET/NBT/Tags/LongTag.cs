using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.NBT.Tags
{
    public class LongTag : DataTag<long>
    {
        public LongTag(long data) : this("", data)
        {

        }

        public LongTag(string name, long data) : base(name, data)
        {

        }

        public override byte TagID
        {
            get
            {
                return TAG_LONG;
            }
        }

        public override string ToString()
        {
            return $"LongTag : Name {this.Name}  : Data {this.Data}";
        }
    }
}
