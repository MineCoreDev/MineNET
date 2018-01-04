using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Utils
{
    public static class Binary
    {
        public static bool ReadBoolean(byte[] buffer, int offset)
        {
            var b = buffer[offset];
            return b > 0;
        }

        public static bool ReadBoolean(byte[] buffer, long offset)
        {
            var b = buffer[offset];
            return b > 0;
        }

        public static byte[] PutBoolean(byte[] buffer, bool value)
        {
            byte[] r = new byte[1];
            r[0] = value ? (byte)1 : (byte)0;
            return buffer.Concat(r).ToArray();
        }

        public static byte ReadByte(byte[] buffer, int offset)
        {
            var b = buffer[offset];
            return b;
        }

        public static byte ReadByte(byte[] buffer, long offset)
        {
            var b = buffer[offset];
            return b;
        }

        public static byte[] PutByte(byte[] buffer, byte value)
        {
            byte[] r = new byte[1];
            r[0] = value;
            return buffer.Concat(r).ToArray();
        }

        public static sbyte ReadSByte(byte[] buffer, int offset)
        {
            var b = (sbyte)(buffer[offset]);
            return b;
        }

        public static sbyte ReadSByte(byte[] buffer, long offset)
        {
            var b = (sbyte)(buffer[offset]);
            return b;
        }

        public static byte[] PutSByte(byte[] buffer, sbyte value)
        {
            byte[] r = new byte[1];
            r[0] = (byte)value;
            return buffer.Concat(r).ToArray();
        }

        public static short ReadShort(byte[] buffer, int offset)
        {
            var b1 = buffer[offset];
            var b2 = buffer[offset + 1];
            return (short)(b1 | b2 << 8);
        }

        public static short ReadShort(byte[] buffer, long offset)
        {
            var b1 = buffer[offset];
            var b2 = buffer[offset + 1];
            return (short)(b1 | b2 << 8);
        }

        public static byte[] PutShort(byte[] buffer, short value)
        {
            byte[] r = new byte[2];
            r[0] = (byte)value;
            r[1] = (byte)(value >> 8);
            return buffer.Concat(r).ToArray();
        }

        public static ushort ReadUShort(byte[] buffer, int offset)
        {
            var b1 = buffer[offset];
            var b2 = buffer[offset + 1];
            return (ushort)(b1 | b2 << 8);
        }

        public static ushort ReadUShort(byte[] buffer, long offset)
        {
            var b1 = buffer[offset];
            var b2 = buffer[offset + 1];
            return (ushort)(b1 | b2 << 8);
        }

        public static byte[] PutUShort(byte[] buffer, ushort value)
        {
            byte[] r = new byte[2];
            r[0] = (byte)value;
            r[1] = (byte)(value >> 8);
            return buffer.Concat(r).ToArray();
        }
    }
}
