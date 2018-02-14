namespace MineNET.NBT.Tags
{
    public abstract class Tag
    {
        public const byte LITTLE_ENDIAN = 0;
        public const byte BIG_ENDIAN = 1;

        public const byte TAG_END = 0;
        public const byte TAG_BYTE = 1;
        public const byte TAG_SHORT = 2;
        public const byte TAG_INT = 3;
        public const byte TAG_LONG = 4;
        public const byte TAG_FLOAT = 5;
        public const byte TAG_DOUBLE = 6;
        public const byte TAG_BYTE_ARRAY = 7;
        public const byte TAG_STRING = 8;
        public const byte TAG_LIST = 9;
        public const byte TAG_COMPOUND = 10;
        public const byte TAG_INT_ARRAY = 11;

        public const byte ID = 0xff;

        private string name = "";

        public Tag(string name)
        {
            this.name = name;
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

        public virtual void Write(NBTStream stream)
        {

        }

        public virtual void Read(NBTStream stream)
        {

        }
    }
}
