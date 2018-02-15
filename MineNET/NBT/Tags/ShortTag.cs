using System;

namespace MineNET.NBT.Tags
{
    public class ShortTag : DataTag<short>
    {
        public override NBTTagType TagType
        {
            get
            {
                return NBTTagType.SHORT;
            }
        }

        public ShortTag(short data) : this("", data)
        {

        }

        public ShortTag(string name, short data) : base(name, data)
        {

        }

        public override string ToString()
        {
            return $"ShortTag : Name {this.Name}  : Data {this.Data}";
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
