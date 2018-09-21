using MineNET.NBT.Data;
using MineNET.NBT.IO;
using System;

namespace MineNET.NBT.Tags
{
    public class IntArrayTag : ArrayDataTag<int>
    {
        public override NBTTagType TagType
        {
            get
            {
                return NBTTagType.INT_ARRAY;
            }
        }

        public IntArrayTag(int[] data) : this("", data)
        {

        }

        public IntArrayTag(string name, int[] data) : base(name, data)
        {

        }

        public override string ToString()
        {
            return $"IntArrayTag : Name {this.Name} : Data {this.Data}";
        }

        internal override void Write(NBTStream stream)
        {
            int len = this.Data.Length;
            stream.WriteInt(len);
            stream.Reservation(len * sizeof(int));
            for (int i = 0; i < len; i++)
            {
                stream.WriteInt(this.Data[i]);
            }
        }

        internal override void WriteTag(NBTStream stream)
        {
            int len = this.Data.Length;
            if (this.Name != null)
            {
                stream.WriteByte((byte) this.TagType);
                stream.WriteString(this.Name);
                stream.WriteInt(len);
                stream.Reservation(len * sizeof(int));
                for (int i = 0; i < len; i++)
                {
                    stream.WriteInt(this.Data[i]);
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
            this.Data = new int[len];
            for (int i = 0; i < len; i++)
            {
                this.Data[i] = stream.ReadInt();
            }
        }

        internal override void ReadTag(NBTStream stream)
        {
            stream.ReadByte();
            string name = stream.ReadString();
            this.Read(stream);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is IntArrayTag))
            {
                return false;
            }
            IntArrayTag tag = (IntArrayTag) obj;
            if (this.Name != tag.Name)
            {
                return false;
            }
            if (this.Data.Length != tag.Data.Length)
            {
                return false;
            }
            for (int i = 0; i < this.Data.Length; i++)
            {
                if (this.Data[i] != tag.Data[i])
                {
                    return false;
                }
            }
            return true;
        }

        public static bool operator ==(IntArrayTag A, IntArrayTag B)
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

        public static bool operator !=(IntArrayTag A, IntArrayTag B)
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
