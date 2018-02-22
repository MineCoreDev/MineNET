namespace MineNET.NBT.Tags
{
    public class DoubleTag : DataTag<double>
    {
        public override NBTTagType TagType
        {
            get
            {
                return NBTTagType.DOUBLE;
            }
        }

        public DoubleTag(double data) : this("", data)
        {

        }

        public DoubleTag(string name, double data) : base(name, data)
        {

        }

        public override string ToString()
        {
            return $"DoubleTag : Name {this.Name} : Data {this.Data}";
        }

        internal override void Write(NBTStream stream)
        {
            stream.WriteDouble(this.Data);
        }

        internal override void WriteTag(NBTStream stream)
        {
            stream.WriteByte((byte) TagType);
            stream.WriteString(this.Name);
            this.Write(stream);
        }

        internal override void Read(NBTStream stream)
        {
            this.Data = stream.ReadDouble();
        }

        internal override void ReadTag(NBTStream stream)
        {
            stream.ReadByte();
            this.Name = stream.ReadString();
            this.Read(stream);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is DoubleTag))
            {
                return false;
            }
            DoubleTag tag = (DoubleTag) obj;
            if (this.Name != tag.Name)
            {
                return false;
            }
            if (this.Data != tag.Data)
            {
                return false;
            }
            return true;
        }

        public static bool operator ==(DoubleTag A, DoubleTag B)
        {
            return A.Equals(B);
        }

        public static bool operator !=(DoubleTag A, DoubleTag B)
        {
            return !A.Equals(B);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
