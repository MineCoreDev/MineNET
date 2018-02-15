using System;

namespace MineNET.NBT.Tags
{
    public class ByteArrayTag : ArrayDataTag<byte>
    {
        public override NBTTagType TagType
        {
            get
            {
                return NBTTagType.BYTE_ARRAY;
            }
        }

        public ByteArrayTag(byte[] data) : this("", data)
        {

        }

        public ByteArrayTag(string name, byte[] data) : base(name, data)
        {

        }

        public override string ToString()
        {
            return $"ByteArrayTag : Name {this.Name}  : Data {this.Data}";
        }

        internal override void Write(NBTStream stream)
        {
            int len = this.Data.Length;
            stream.WriteInt(len);
            for (int i = 0; i < len; ++i)
            {
                stream.WriteByte(this.Data[i]);
            }
        }

        internal override void WriteTag(NBTStream stream)
        {
            int len = this.Data.Length;
            if (Name != null)
            {
                stream.WriteByte((byte)TagType);
                stream.WriteString(Name);
                stream.WriteInt(len);
                for (int i = 0; i < len; ++i)
                {
                    stream.WriteByte(this.Data[i]);
                }
            }
            else
            {
                throw new NullReferenceException("Tag Name Null");
            }
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
