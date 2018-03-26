using System;
using MineNET.NBT.Data;
using MineNET.NBT.IO;

namespace MineNET.NBT.Tags
{
    public class LongTag : DataTag<long>
    {
        public override NBTTagType TagType
        {
            get
            {
                return NBTTagType.LONG;
            }
        }

        public LongTag(long data) : this("", data)
        {

        }

        public LongTag(string name, long data) : base(name, data)
        {

        }

        public override string ToString()
        {
            return $"LongTag : Name {this.Name} : Data {this.Data}";
        }

        internal override void Write(NBTStream stream)
        {
            stream.WriteLong(this.Data);
        }

        internal override void WriteTag(NBTStream stream)
        {
            if (this.Name != null)
            {
                stream.WriteByte((byte) TagType);
                stream.WriteString(this.Name);
                this.Write(stream);
            }
            else
            {
                throw new NullReferenceException("Tag Name Null");
            }
        }

        internal override void Read(NBTStream stream)
        {
            this.Data = stream.ReadLong();
        }

        internal override void ReadTag(NBTStream stream)
        {
            stream.ReadByte();
            this.Name = stream.ReadString();
            this.Read(stream);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is LongTag))
            {
                return false;
            }
            LongTag tag = (LongTag) obj;
            if (this.Name != tag.Name)
            {
                return false;
            }
            if (this.Data != tag.Data)
            {
                return false;
            }
            return true;
        }

        public static bool operator ==(LongTag A, LongTag B)
        {
            return A.Equals(B);
        }

        public static bool operator !=(LongTag A, LongTag B)
        {
            return !A.Equals(B);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
