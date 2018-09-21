using MineNET.NBT.Data;
using MineNET.NBT.IO;
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
            return $"ByteArrayTag : Name {this.Name} : Data {this.Data}";
        }

        internal override void Write(NBTStream stream)
        {
            int len = this.Data.Length;
            stream.WriteInt(len);
            stream.Reservation(len);
            stream.WriteBytes(this.Data);
        }

        internal override void WriteTag(NBTStream stream)
        {
            int len = this.Data.Length;
            if (this.Name != null)
            {
                stream.WriteByte((byte) this.TagType);
                stream.WriteString(this.Name);
                stream.WriteInt(len);
                stream.Reservation(len);
                stream.WriteBytes(this.Data);
            }
            else
            {
                throw new NullReferenceException("Tag Name Null");
            }
        }

        internal override void Read(NBTStream stream)
        {
            stream.ReadInt();
            this.Data = stream.ReadBytes();
        }

        internal override void ReadTag(NBTStream stream)
        {
            stream.ReadByte();
            string name = stream.ReadString();
            this.Read(stream);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ByteArrayTag))
            {
                return false;
            }
            ByteArrayTag tag = (ByteArrayTag) obj;
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

        public static bool operator ==(ByteArrayTag A, ByteArrayTag B)
        {
            if (object.ReferenceEquals(A, B))
            {
                return true;
            }
            if ((object) A == null || (object) B == null)
            {
                return false;
            }
            return A.Equals(B);
        }

        public static bool operator !=(ByteArrayTag A, ByteArrayTag B)
        {
            if (object.ReferenceEquals(A, B))
            {
                return false;
            }
            if ((object) A == null || (object) B == null)
            {
                return true;
            }
            return !A.Equals(B);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
