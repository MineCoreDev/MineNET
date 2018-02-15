using System;

namespace MineNET.NBT.Tags
{
    public class IntArrayTag : ArrayDataTag<int>
    {
        public override NBTTagType TagType
        {
            get
            {
                return NBTTagType.INT_ARRAY;
            }
        }

        public IntArrayTag(int[] data) : this("", data)
        {

        }

        public IntArrayTag(string name, int[] data) : base(name, data)
        {

        }

        public override string ToString()
        {
            return $"IntArrayTag : Name {this.Name}  : Data {this.Data}";
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
