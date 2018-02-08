using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.NBT.Tags
{
    public class StringTag : DataTag<string>
    {
        public StringTag(String data) : this("", data)
        {

        }

        public StringTag(String name, String data) : base(name, data)
        {
            
        }

        public override byte TagID
        {
            get
            {
                return TAG_STRING;
            }
        }

        public override string ToString()
        {
            return $"StringTag : Name {this.Name}  : Data {this.Data}";
        }
    }
}
