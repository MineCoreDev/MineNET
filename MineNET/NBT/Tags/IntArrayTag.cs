using System;
using MineNET.NBT.Data;
using MineNET.NBT.IO;

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
            for (int i = 0; i < this.Data.Length; ++i)
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
            return A.Equals(B);
        }

        public static bool operator !=(IntArrayTag A, IntArrayTag B)
        {
            return !A.Equals(B);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
