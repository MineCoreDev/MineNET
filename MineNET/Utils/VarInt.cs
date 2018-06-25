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

        private static uint ReadRawVarInt32(ref MemorySpan span, int maxSize)
        {
            uint result = 0;
            int j = 0;
            int b0;

            do
            {
                b0 = span.ReadByte();
                if (b0 < 0) throw new EndOfStreamException("Not enough bytes for VarInt");

                result |= (uint) (b0 & 0x7f) << j++ * 7;

                if (j > maxSize)
                {
                    throw new OverflowException("VarInt too big");
                }
            } while ((b0 & 0x80) == 0x80);

            return result;
        }

        private static ulong ReadRawVarInt64(ref MemorySpan span, int maxSize)
        {
            List<byte> bytes = new List<byte>();

            ulong result = 0;
            int j = 0;
            int b0;

            do
            {
                b0 = span.ReadByte();
                bytes.Add((byte) b0);
                if (b0 < 0) throw new EndOfStreamException("Not enough bytes for VarInt");

                result |= (ulong) (b0 & 0x7f) << j++ * 7;

                if (j > maxSize)
                {
                    throw new OverflowException("VarInt too big");
                }
            } while ((b0 & 0x80) == 0x80);

            byte[] byteArray = bytes.ToArray();

            return result;
        }

        private static void WriteRawVarInt32(ref MemorySpan span, uint value)
        {
            while ((value & -128) != 0)
            {
                span.WriteByte((byte) ((value & 0x7F) | 0x80));
                value >>= 7;
            }

            span.WriteByte((byte) value);
        }

        private static void WriteRawVarInt64(ref MemorySpan span, ulong value)
        {
            while ((value & 0xFFFFFFFFFFFFFF80) != 0)
            {
                span.WriteByte((byte) ((value & 0x7F) | 0x80));
                value >>= 7;
            }

            span.WriteByte((byte) value);
        }

        public static void WriteInt32(ref MemorySpan span, int value)
        {
            WriteRawVarInt32(ref span, (uint) value);
        }

        public static int ReadInt32(ref MemorySpan span)
        {
            return (int) ReadRawVarInt32(ref span, 5);
        }

        public static void WriteSInt32(ref MemorySpan span, int value)
        {
            WriteRawVarInt32(ref span, EncodeZigZag32(value));
        }

        public static int ReadSInt32(ref MemorySpan span)
        {
            return DecodeZigZag32(ReadRawVarInt32(ref span, 5));
        }

        public static void WriteUInt32(ref MemorySpan span, uint value)
        {
            WriteRawVarInt32(ref span, value);
        }

        public static uint ReadUInt32(ref MemorySpan span)
        {
            return ReadRawVarInt32(ref span, 5);
        }

        public static void WriteInt64(ref MemorySpan span, long value)
        {
            WriteRawVarInt64(ref span, (ulong) value);
        }

        public static long ReadInt64(ref MemorySpan span)
        {
            return (long) ReadRawVarInt64(ref span, 10);
        }

        public static void WriteSInt64(ref MemorySpan span, long value)
        {
            WriteRawVarInt64(ref span, EncodeZigZag64(value));
        }

        public static long ReadSInt64(ref MemorySpan span)
        {
            return DecodeZigZag64(ReadRawVarInt64(ref span, 10));
        }

        public static void WriteUInt64(ref MemorySpan span, ulong value)
        {
            WriteRawVarInt64(ref span, value);
        }

        public static ulong ReadUInt64(ref MemorySpan span)
        {
            return ReadRawVarInt64(ref span, 10);
        }
    }
}
