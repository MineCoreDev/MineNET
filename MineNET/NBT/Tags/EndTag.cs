using System;
using MineNET.NBT.Data;
using MineNET.NBT.IO;

namespace MineNET.NBT.Tags
{
    public class EndTag : Tag
    {
        public override NBTTagType TagType
        {
            get
            {
                return NBTTagType.END;
            }
        }

        public EndTag() : base(null)
        {

        }

        public override string ToString()
        {
            return "EndTag";
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
            if (!(obj is EndTag))
            {
                return false;
            }
            return true;
        }

        public static bool operator ==(EndTag A, EndTag B)
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

        public static bool operator !=(EndTag A, EndTag B)
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
