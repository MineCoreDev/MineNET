using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.NBT.Tags
{
    public class ByteArrayTag : Tag
    {
        public new const byte ID = TAG_BYTE_ARRAY;

        private byte[] data;

        public ByteArrayTag(byte[] data) : this("", data)
        {

        }

        public ByteArrayTag(string name, byte[] data) : base(name)
        {
            this.data = data;
        }

        public byte[] Data
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

        public override string ToString()
        {
            return $"ByteArrayTag : Name {this.Name}  : Data {this.Data}";
        }
    }
}
