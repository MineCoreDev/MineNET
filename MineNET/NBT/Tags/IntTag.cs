﻿namespace MineNET.NBT.Tags
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
            stream.WriteInt(this.Data);
        }

        internal override void WriteTag(NBTStream stream)
        {
            stream.WriteByte((byte) TagType);
            stream.WriteString(this.Name);
            this.Write(stream);
        }

        internal override void Read(NBTStream stream)
        {
            this.Data = stream.ReadInt();
        }

        internal override void ReadTag(NBTStream stream)
        {
            stream.ReadByte();
            this.Name = stream.ReadString();
            this.Read(stream);
        }
    }
}
