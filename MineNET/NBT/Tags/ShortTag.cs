using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.NBT.Tags
{
    public class ShortTag : DataTag<short>
    {
        public ShortTag(short data) : this("", data)
        {

        }

        public ShortTag(string name, short data) : base(name, data)
        {
            
        }

        public override byte TagID
        {
            get
            {
                return TAG_SHORT;
            }
        }

        public override string ToString()
        {
            return $"ShortTag : Name {this.Name}  : Data {this.Data}";
        }
    }
}
