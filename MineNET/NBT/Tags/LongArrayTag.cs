using System;
using MineNET.NBT.Data;
using MineNET.NBT.IO;

namespace MineNET.NBT.Tags
{
    public class LongArrayTag : ArrayDataTag<long>
    {
        public override NBTTagType TagType
        {
            get
            {
                return NBTTagType.LONG_ARRAY;
            }
        }

        public LongArrayTag(long[] data) : this("", data)
        {

        }

        public LongArrayTag(string name, long[] data) : base(name, data)
        {

        }

        public override string ToString()
        {
            return $"ByteArrayTag : Name {this.Name} : Data {this.Data}";
        }

        internal override void Write(NBTStream stream)
        {
            int len = this.Data.Length;
            stream.WriteInt(len);
            for (int i = 0; i < len; ++i)
            {
                stream.WriteLong(this.Data[i]);
            }
        }

        internal override void WriteTag(NBTStream stream)
        {
            int len = this.Data.Length;
            if (!string.IsNullOrEmpty(this.Name))
            {
                stream.WriteByte((byte) this.TagType);
                stream.WriteString(this.Name);
                stream.WriteInt(len);
                for (int i = 0; i < len; ++i)
                {
                    stream.WriteLong(this.Data[i]);
                }
            }
            else
            {
                throw new NullReferenceException("Tag Name Null");
            }
        }

        internal override void Read(NBTStream stream)
        {
            int len = stream.ReadInt();
            this.Data = new long[len];
            for (int i = 0; i < len; ++i)
            {
                this.Data[i] = stream.ReadLong();
            }
        }

        internal override void ReadTag(NBTStream stream)
        {
            stream.ReadByte();
            string name = stream.ReadString();
            int len = stream.ReadInt();
            this.Data = new long[len];
            for (int i = 0; i < len; ++i)
            {
                this.Data[i] = stream.ReadLong();
            }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is LongArrayTag))
            {
                return false;
            }
            LongArrayTag tag = (LongArrayTag) obj;
            if (this.Name != tag.Name)
            {
                return false;
            }
            if (this.Data.Length != tag.Data.Length)
            {
                return false;
            }
            for (int i = 0; i < this.Data.Length; ++i)
            {
                if (this.Data[i] != tag.Data[i])
                {
                    return false;
                }
            }
            return true;
        }

        public static bool operator ==(LongArrayTag A, LongArrayTag B)
        {
            return A.Equals(B);
        }

        public static bool operator !=(LongArrayTag A, LongArrayTag B)
        {
            return !A.Equals(B);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
