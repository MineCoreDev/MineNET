using System;
using MineNET.NBT.Data;
using MineNET.NBT.IO;
using MineNET.Utils;

namespace MineNET.NBT.Tags
{
    public abstract class Tag : ICloneable<Tag>
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

        public Tag Clone()
        {
            return (Tag) Clone();
        }

        object ICloneable.Clone()
        {
            return this.MemberwiseClone();
        }

        internal abstract void Write(NBTStream stream);
        internal abstract void WriteTag(NBTStream stream);

        internal abstract void Read(NBTStream stream);
        internal abstract void ReadTag(NBTStream stream);
    }
}
