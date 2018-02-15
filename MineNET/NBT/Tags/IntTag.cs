using System;

namespace MineNET.NBT.Tags
{
    public class IntTag : DataTag<int>
    {
        public override NBTTagType TagType
        {
            get
            {
                return NBTTagType.INT;
            }
        }

        public IntTag(int data) : this("", data)
        {

        }

        public IntTag(string name, int data) : base(name, data)
        {

        }

        public override string ToString()
        {
            return $"IntTag : Name {this.Name}  : Data {this.Data}";
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
