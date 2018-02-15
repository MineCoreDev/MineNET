using System;

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
            return $"LongTag : Name {this.Name}  : Data {this.Data}";
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
