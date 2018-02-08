using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.NBT.Tags
{
    public class IntTag : DataTag<int>
    {
        private int data;

        public IntTag(int data) : this("", data)
        {
            
        }

        public IntTag(string name, int data) : base(name, data)
        {
            
        }

        public override byte TagID
        {
            get
            {
                return TAG_INT;
            }
        }

        public override string ToString()
        {
            return $"IntTag : Name {this.Name}  : Data {this.Data}";
        }
    }
}
