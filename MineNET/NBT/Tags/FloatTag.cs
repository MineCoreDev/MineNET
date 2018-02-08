namespace MineNET.NBT.Tags
{
    public class FloatTag : DataTag<float>
    {
        public new const byte ID = TAG_FLOAT;

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
    }
}
