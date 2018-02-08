using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.NBT.Tags
{
    public class DoubleTag : DataTag<double>
    {
        public DoubleTag(double data) : this("", data)
        {

        }

        public DoubleTag(string name, double data) : base(name, data)
        {

        }

        public override byte TagID
        {
            get
            {
                return TAG_DOUBLE;
            }
        }

        public override string ToString()
        {
            return $"DoubleTag : Name {this.Name}  : Data {this.Data}";
        }
    }
}
