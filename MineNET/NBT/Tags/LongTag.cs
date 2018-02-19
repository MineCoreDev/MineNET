namespace MineNET.NBT.Tags
{
    public class LongTag : DataTag<long>
    {
        public override NBTTagType TagType
        {
            get
            {
                return NBTTagType.LONG;
            }
        }

        public LongTag(long data) : this("", data)
        {

        }

        public LongTag(string name, long data) : base(name, data)
        {

        }

        public override string ToString()
        {
            return $"LongTag : Name {this.Name}  : Data {this.Data}";
        }

        internal override void Write(NBTStream stream)
        {
            stream.WriteLong(this.Data);
        }

        internal override void WriteTag(NBTStream stream)
        {
            stream.WriteByte((byte) TagType);
            stream.WriteString(this.Name);
            this.Write(stream);
        }

        internal override void Read(NBTStream stream)
        {
            this.Data = stream.ReadLong();
        }

        internal override void ReadTag(NBTStream stream)
        {
            stream.ReadByte();
            this.Name = stream.ReadString();
            this.Read(stream);
        }
    }
}
