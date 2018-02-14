using System;

namespace MineNET.NBT.Tags
{
    public class FloatTag : DataTag<float>
    {
        public new const byte ID = TAG_FLOAT;

        public FloatTag(float data) : this("", data)
        {

        }

        public FloatTag(string name, float data) : base(name, data)
        {

        }

        public override void Read(NBTStream stream)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return $"FloatTag : Name {this.Name}  : Data {this.Data}";
        }

        public override void Write(NBTStream stream)
        {
            throw new NotImplementedException();
        }
    }
}
