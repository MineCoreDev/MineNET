using System;

namespace MineNET.NBT.Tags
{
    public class ByteTag : DataTag<byte>
    {
        public override NBTTagType TagType
        {
            get
            {
                return NBTTagType.BYTE;
            }
        }

        public ByteTag(byte data) : this("", data)
        {

        }

        public ByteTag(string name, byte data) : base(name, data)
        {

        }

        public override string ToString()
        {
            return $"ByteTag : Name {this.Name}  : Data {this.Data}";
        }

        internal override void Write(NBTStream stream)
        {
            throw new NotImplementedException();
        }

        internal override void WriteTag(NBTStream stream)
        {
            throw new NotImplementedException();
        }

        internal override void Read(NBTStream stream)
        {
            throw new NotImplementedException();
        }

        internal override void ReadTag(NBTStream stream)
        {
            throw new NotImplementedException();
        }
    }
}
