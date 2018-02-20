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
            stream.WriteString(this.Data);
        }

        internal override void WriteTag(NBTStream stream)
        {
            stream.WriteByte((byte) TagType);
            stream.WriteString(this.Name);
            this.Write(stream);
        }

        internal override void Read(NBTStream stream)
        {
            this.Data = stream.ReadString();
        }

        internal override void ReadTag(NBTStream stream)
        {
            stream.ReadByte();
            this.Name = stream.ReadString();
            this.Read(stream);
        }
    }
}
