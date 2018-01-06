using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.NBT.Tags
{
    class ByteTag : Tag
    {
        private byte data;

        public ByteTag(byte data) : this("", data)
        {

        }

        public ByteTag(string name, byte data) : base(name)
        {
            this.data = data;
        }

        public byte Data
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
    }
}
