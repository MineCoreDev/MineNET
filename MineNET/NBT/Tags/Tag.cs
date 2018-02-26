using MineNET.NBT.Data;
using MineNET.NBT.IO;

namespace MineNET.NBT.Tags
{
    public abstract class Tag
    {
        private string name = "";

        public Tag(string name)
        {
            this.name = name;
        }

        public abstract NBTTagType TagType
        {
            get;
        }

        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                this.name = value;
            }
        }

        internal abstract void Write(NBTStream stream);
        internal abstract void WriteTag(NBTStream stream);

        internal abstract void Read(NBTStream stream);
        internal abstract void ReadTag(NBTStream stream);
    }
}
