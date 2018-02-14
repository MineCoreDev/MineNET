namespace MineNET.NBT.Tags
{
    public class ShortTag : DataTag<short>
    {
        public new const byte ID = TAG_SHORT;

        public ShortTag(short data) : this("", data)
        {

        }

        public ShortTag(string name, short data) : base(name, data)
        {

        }

        public override string ToString()
        {
            return $"ShortTag : Name {this.Name}  : Data {this.Data}";
        }
    }
}
