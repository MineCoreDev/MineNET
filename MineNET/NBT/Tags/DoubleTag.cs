namespace MineNET.NBT.Tags
{
    public class DoubleTag : DataTag<double>
    {
        public new const byte ID = TAG_DOUBLE;

        public DoubleTag(double data) : this("", data)
        {

        }

        public DoubleTag(string name, double data) : base(name, data)
        {

        }

        public override string ToString()
        {
            return $"DoubleTag : Name {this.Name}  : Data {this.Data}";
        }
    }
}
