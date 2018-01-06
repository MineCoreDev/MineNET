using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.NBT.Tags
{
    public abstract class Tag
    {
        public const int LITTLE_ENDIAN = 0;
        public const int BIG_ENDIAN = 1;
        public const int TAG_End = 0;
        public const int TAG_Byte = 1;
        public const int TAG_Short = 2;
        public const int TAG_Int = 3;
        public const int TAG_Long = 4;
        public const int TAG_Float = 5;
        public const int TAG_Double = 6;
        public const int TAG_ByteArray = 7;
        public const int TAG_String = 8;
        public const int TAG_List = 9;
        public const int TAG_Compound = 10;
        public const int TAG_IntArray = 11;

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
    }
}
