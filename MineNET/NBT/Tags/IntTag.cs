using System;

namespace MineNET.NBT.Tags
{
    public class IntTag : DataTag<int>
    {
        public new const byte ID = TAG_INT;

        public IntTag(int data) : this("", data)
        {

        }

        public IntTag(string name, int data) : base(name, data)
        {

        }

        public override void Read(NBTStream stream)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return $"IntTag : Name {this.Name}  : Data {this.Data}";
        }

        public override void Write(NBTStream stream)
        {
            throw new NotImplementedException();
        }
    }
}
