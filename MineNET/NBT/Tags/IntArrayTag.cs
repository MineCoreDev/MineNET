using System;

namespace MineNET.NBT.Tags
{
    public class IntArrayTag : Tag
    {
        public new const byte ID = TAG_INT_ARRAY;

        private int[] data;

        public IntArrayTag(int[] data) : this("", data)
        {

        }

        public IntArrayTag(string name, int[] data) : base(name)
        {
            this.data = data;
        }

        public int[] Data
        {
            get
            {
                return this.data;
            }

            set
            {
                this.data = value;
            }
        }

        public override void Read(NBTStream stream)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return $"IntArrayTag : Name {this.Name}  : Data {this.Data}";
        }

        public override void Write(NBTStream stream)
        {
            throw new NotImplementedException();
        }
    }
}
