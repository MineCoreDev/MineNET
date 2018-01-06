using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Values
{
    public struct Int24
    {
        public const int MaxValue = 16777215;
        public const int MinValue = -16777215;

        private int f_value;

        public Int24(int value)
        {
            this.f_value = CheckInt24(value);
        }

        public Int24(byte[] value)
        {
            this.f_value = ByteArray3ToInt32(value);
        }

        public byte[] ToBytes()
        {
            return Int32ToByteArray3(this.f_value);
        }

        public override string ToString()
        {
            return this.f_value.ToString();
        }

        public static Int24 Parse(int value)
        {
            return new Int24(CheckInt24(value));
        }

        public static Int24 Parse(byte[] value)
        {
            return new Int24(ByteArray3ToInt32(value));
        }

        private static int ByteArray3ToInt32(byte[] value)
        {
            if (value.Length > 3) throw new Exception();
            return value[0] | value[1] << 8 | value[2] << 16;
        }

        private static byte[] Int32ToByteArray3(int value)
        {
            byte[] bytes = new byte[3];
            bytes[0] = (byte)value;
            bytes[1] = (byte)(value >> 8);
            bytes[2] = (byte)(value >> 16);
            return bytes;
        }

        private static int CheckInt24(int value)
        {
            if (value >= MinValue && value <= MaxValue)
            {
                return value;
            }
            else throw new Exception();
        }
    }
}
