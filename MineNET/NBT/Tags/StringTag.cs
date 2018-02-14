using System;

namespace MineNET.NBT.Tags
{
    public class StringTag : DataTag<string>
    {
        public new const byte ID = TAG_STRING;

        public StringTag(String data) : this("", data)
        {

        }

        public StringTag(String name, String data) : base(name, data)
        {

        }

        public override void Read(NBTStream stream)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return $"StringTag : Name {this.Name}  : Data {this.Data}";
        }

        public override void Write(NBTStream stream)
        {
            throw new NotImplementedException();
        }
    }
}
