using System;
using System.Collections.Generic;
using System.IO;

namespace MineNET.Utils
{
    public static class VarInt
    {
        private static uint EncodeZigZag32(int n)
        {
            // Note:  the right-shift must be arithmetic
            return (uint) ((n << 1) ^ (n >> 31));
        }

        private static int DecodeZigZag32(uint n)
        {
            return (int) (n >> 1) ^ -(int) (n & 1);
        }

        private static ulong EncodeZigZag64(long n)
        {
            return (ulong) ((n << 1) ^ (n >> 63));
        }

        private static long DecodeZigZag64(ulong n)
        {
            return (long) (n >> 1) ^ -(long) (n & 1);
        }

        private static uint ReadRawVarInt32(List<byte> buffer, ref int offset, int maxSize)
        {
            uint result = 0;
            int j = 0;
            int b0;

            do
            {
                b0 = buffer[offset];
                if (b0 < 0) throw new EndOfStreamException("Not enough bytes for VarInt");

                result |= (uint) (b0 & 0x7f) << j++ * 7;

                if (j > maxSize)
                {
                    throw new OverflowException("VarInt too big");
                }
                offset++;
            } while ((b0 & 0x80) == 0x80);

            return result;
        }

        private static ulong ReadRawVarInt64(List<byte> buffer, ref int offset, int maxSize)
        {
            List<byte> bytes = new List<byte>();

            ulong result = 0;
            int j = 0;
            int b0;

            do
            {
                b0 = buffer[offset];
                bytes.Add((byte) b0);
                if (b0 < 0) throw new EndOfStreamException("Not enough bytes for VarInt");

                result |= (ulong) (b0 & 0x7f) << j++ * 7;

                if (j > maxSize)
                {
                    throw new OverflowException("VarInt too big");
                }
                offset++;
            } while ((b0 & 0x80) == 0x80);

            byte[] byteArray = bytes.ToArray();

            return result;
        }

        private static void WriteRawVarInt32(List<byte> buffer, uint value, out int moveOffset)
        {
            moveOffset = 0;
            while ((value & -128) != 0)
            {
                buffer.Add((byte) ((value & 0x7F) | 0x80));
                value >>= 7;
                moveOffset++;
            }

            buffer.Add((byte) value);
            moveOffset++;
        }

        private static void WriteRawVarInt64(List<byte> buffer, ulong value, out int moveOffset)
        {
            moveOffset = 0;
            while ((value & 0xFFFFFFFFFFFFFF80) != 0)
            {
                buffer.Add((byte) ((value & 0x7F) | 0x80));
                value >>= 7;
                moveOffset++;
            }

            buffer.Add((byte) value);
            moveOffset++;
        }

        public static void WriteInt32(List<byte> buffer, int value, out int moveOffset)
        {
            WriteRawVarInt32(buffer, (uint) value, out moveOffset);
        }

        public static int ReadInt32(List<byte> buffer, ref int offset)
        {
            return (int) ReadRawVarInt32(buffer, ref offset, 5);
        }

        public static void WriteSInt32(List<byte> buffer, int value, out int moveOffset)
        {
            WriteRawVarInt32(buffer, EncodeZigZag32(value), out moveOffset);
        }

        public static int ReadSInt32(List<byte> buffer, ref int offset)
        {
            return DecodeZigZag32(ReadRawVarInt32(buffer, ref offset, 5));
        }

        public static void WriteUInt32(List<byte> buffer, uint value, out int moveOffset)
        {
            WriteRawVarInt32(buffer, value, out moveOffset);
        }

        public static uint ReadUInt32(List<byte> buffer, ref int offset)
        {
            return ReadRawVarInt32(buffer, ref offset, 5);
        }

        public static void WriteInt64(List<byte> buffer, long value, out int moveOffset)
        {
            WriteRawVarInt64(buffer, (ulong) value, out moveOffset);
        }

        public static long ReadInt64(List<byte> buffer, ref int offset)
        {
            return (long) ReadRawVarInt64(buffer, ref offset, 10);
        }

        public static void WriteSInt64(List<byte> buffer, long value, out int moveOffset)
        {
            WriteRawVarInt64(buffer, EncodeZigZag64(value), out moveOffset);
        }

        public static long ReadSInt64(List<byte> buffer, ref int offset)
        {
            return DecodeZigZag64(ReadRawVarInt64(buffer, ref offset, 10));
        }

        public static void WriteUInt64(List<byte> buffer, ulong value, out int moveOffset)
        {
            WriteRawVarInt64(buffer, value, out moveOffset);
        }

        public static ulong ReadUInt64(List<byte> buffer, ref int offset)
        {
            return ReadRawVarInt64(buffer, ref offset, 10);
        }
    }
}
