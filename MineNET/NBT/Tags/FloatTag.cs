using System;

namespace MineNET.NBT.Tags
{
    public class FloatTag : DataTag<float>
    {
        public override NBTTagType TagType
        {
            get
            {
                return NBTTagType.FLOAT;
            }
        }

        public FloatTag(float data) : this("", data)
        {

        }

        public FloatTag(string name, float data) : base(name, data)
        {

        }

        public override string ToString()
        {
            return $"FloatTag : Name {this.Name}  : Data {this.Data}";
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
