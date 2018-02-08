using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.NBT.Tags
{
    public class IntArrayTag : Tag
    {
        private int[] data;
    
        public IntArrayTag(int[] data) : this("", data)
        {

        }

        public IntArrayTag(string name, int[] data) : base(name)
        {
            this.data = data;
        }

        public int[] Data
        {
            get
            {
                return this.data;
            }

            set
            {
                this.data = value;
            }
        }

        public override byte TagID
        {
            get
            {
                return TAG_INT_ARRAY;
            }
        }

        public override string ToString()
        {
            return $"IntArrayTag : Name {this.Name}  : Data {this.Data}";
        }
    }
}
