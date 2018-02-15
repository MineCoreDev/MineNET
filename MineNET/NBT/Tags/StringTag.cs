using System;

namespace MineNET.NBT.Tags
{
    public class StringTag : DataTag<string>
    {
        public override NBTTagType TagType
        {
            get
            {
                return NBTTagType.STRING;
            }
        }

        public StringTag(String data) : this("", data)
        {

        }

        public StringTag(String name, String data) : base(name, data)
        {

        }

        public override string ToString()
        {
            return $"StringTag : Name {this.Name}  : Data {this.Data}";
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
