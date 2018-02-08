using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.NBT.Tags
{
    public class FloatTag : DataTag<float>
    {
        public FloatTag(float data) : this("", data)
        {

        }

        public FloatTag(string name, float data) : base(name, data)
        {
            
        }

        public override byte TagID
        {
            get
            {
                return TAG_FLOAT;
            }
        }

        public override string ToString()
        {
            return $"FloatTag : Name {this.Name}  : Data {this.Data}";
        }
    }
}
