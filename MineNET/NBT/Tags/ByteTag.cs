using System;

namespace MineNET.NBT.Tags
{
    public class ByteTag : DataTag<byte>
    {
        public new const byte ID = TAG_BYTE;

        public ByteTag(byte data) : this("", data)
        {

        }

        public ByteTag(string name, byte data) : base(name, data)
        {

        }

        public override void Read(NBTStream stream)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return $"ByteTag : Name {this.Name}  : Data {this.Data}";
        }

        public override void Write(NBTStream stream)
        {
            throw new NotImplementedException();
        }
    }
}
