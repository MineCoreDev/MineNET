using System;

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
    }
}
