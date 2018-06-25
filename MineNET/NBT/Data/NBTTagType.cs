using System;

namespace MineNET.NBT.Data
{
    public enum NBTTagType
    {
        END = 0,
        BYTE = 1,
        SHORT = 2,
        INT = 3,
        LONG = 4,
        FLOAT = 5,
        DOUBLE = 6,
        BYTE_ARRAY = 7,
        STRING = 8,
        LIST = 9,
        COMPOUND = 10,
        INT_ARRAY = 11,
        LONG_ARRAY = 12
    }

    public static class NBTTagTypeExtension
    {
        public static int ToInt(this NBTTagType type)
        {
            return (int) type;
        }

        public static string ToNameString(this NBTTagType type)
        {
            switch (type)
            {
                case NBTTagType.END:
                    return "End";

                case NBTTagType.BYTE:
                    return "Byte";

                case NBTTagType.SHORT:
                    return "Short";

                case NBTTagType.INT:
                    return "Int";

                case NBTTagType.LONG:
                    return "Long";

                case NBTTagType.FLOAT:
                    return "Float";

                case NBTTagType.DOUBLE:
                    return "Double";

                case NBTTagType.BYTE_ARRAY:
                    return "ByteArray";

                case NBTTagType.STRING:
                    return "String";

                case NBTTagType.LIST:
                    return "List";

                case NBTTagType.COMPOUND:
                    return "Compound";

                case NBTTagType.INT_ARRAY:
                    return "IntArray";

                case NBTTagType.LONG_ARRAY:
                    return "LongArray";

                default:
                    throw new NotSupportedException();
            }
        }

        public static NBTTagType ToTagType(int type)
        {
            switch (type)
            {
                case 0:
                    return NBTTagType.END;

                case 1:
                    return NBTTagType.BYTE;

                case 2:
                    return NBTTagType.SHORT;

                case 3:
                    return NBTTagType.INT;

                case 4:
                    return NBTTagType.LONG;

                case 5:
                    return NBTTagType.FLOAT;

                case 6:
                    return NBTTagType.DOUBLE;

                case 7:
                    return NBTTagType.BYTE_ARRAY;

                case 8:
                    return NBTTagType.STRING;

                case 9:
                    return NBTTagType.LIST;

                case 10:
                    return NBTTagType.COMPOUND;

                case 11:
                    return NBTTagType.INT_ARRAY;

                case 12:
                    return NBTTagType.LONG_ARRAY;

                default:
                    throw new NotSupportedException();
            }
        }

        public static NBTTagType ToTagType(string type)
        {
            switch (type)
            {
                case "End":
                    return NBTTagType.END;

                case "Byte":
                    return NBTTagType.BYTE;

                case "Short":
                    return NBTTagType.SHORT;

                case "Int":
                    return NBTTagType.INT;

                case "Long":
                    return NBTTagType.LONG;

                case "Float":
                    return NBTTagType.FLOAT;

                case "Double":
                    return NBTTagType.DOUBLE;

                case "ByteArray":
                    return NBTTagType.BYTE_ARRAY;

                case "String":
                    return NBTTagType.STRING;

                case "List":
                    return NBTTagType.LIST;

                case "Compound":
                    return NBTTagType.COMPOUND;

                case "IntArray":
                    return NBTTagType.INT_ARRAY;

                case "LongArray":
                    return NBTTagType.LONG_ARRAY;

                default:
                    throw new NotSupportedException();
            }
        }
    }
}
