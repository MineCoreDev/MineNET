using System;

namespace MineNET.NBT.Tags
{
    public class LongTag : DataTag<long>
    {
        public new const byte ID = TAG_LONG;

        public LongTag(long data) : this("", data)
        {

        }

        public LongTag(string name, long data) : base(name, data)
        {

        }

        public override void Read(NBTStream stream)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return $"LongTag : Name {this.Name}  : Data {this.Data}";
        }

        public override void Write(NBTStream stream)
        {
            throw new NotImplementedException();
        }
    }
}
