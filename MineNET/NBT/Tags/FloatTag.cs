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
            stream.WriteFloat(this.Data);
        }

        internal override void WriteTag(NBTStream stream)
        {
            stream.WriteByte((byte) TagType);
            stream.WriteString(this.Name);
            this.Write(stream);
        }

        internal override void Read(NBTStream stream)
        {
            this.Data = stream.ReadFloat();
        }

        internal override void ReadTag(NBTStream stream)
        {
            stream.ReadByte();
            this.Name = stream.ReadString();
            this.Read(stream);
        }
    }
}
