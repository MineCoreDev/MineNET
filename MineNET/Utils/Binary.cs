using System;
using System.Collections.Generic;
using System.Text;

namespace MineNET.Utils
{
    public sealed class Binary
    {
        public const int Int24_Max = 16777215;

        public static bool ReadBool(ref MemorySpan span)
        {
            byte[] bytes = span.ReadBytes(1);

            return BitConverter.ToBoolean(bytes, 0);
        }

        public static void WriteBool(ref MemorySpan span, bool value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            span.WriteBytes(bytes);
        }

        public static byte ReadByte(ref MemorySpan span)
        {
            return span.ReadByte();
        }

        public static void WriteByte(ref MemorySpan span, byte value)
        {
            span.WriteByte(value);
        }

        public static sbyte ReadSByte(ref MemorySpan span)
        {
            return (sbyte) span.ReadByte();
        }

        public static void WriteSByte(ref MemorySpan span, sbyte value)
        {
            span.WriteByte((byte) value);
        }

        public static short ReadShort(ref MemorySpan span)
        {
            byte[] bytes = span.ReadBytes(2);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            return BitConverter.ToInt16(bytes, 0);
        }

        public static void WriteShort(ref MemorySpan span, short value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            span.WriteBytes(bytes);
        }

        public static ushort ReadUShort(ref MemorySpan span)
        {
            byte[] bytes = span.ReadBytes(2);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            return BitConverter.ToUInt16(bytes, 0);
        }

        public static void WriteUShort(ref MemorySpan span, ushort value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            span.WriteBytes(bytes);
        }

        public static ushort ReadLShort(ref MemorySpan span)
        {
            byte[] bytes = span.ReadBytes(2);

            if (!BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            return BitConverter.ToUInt16(bytes, 0);
        }

        public static void WriteLShort(ref MemorySpan span, ushort value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            if (!BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            span.WriteBytes(bytes);
        }

        public static int ReadTriad(ref MemorySpan span)
        {
            byte[] bytes = span.ReadBytes(3);

            return (bytes[0] << 16 | bytes[1] << 8 | bytes[2]);
        }

        public static void WriteTriad(ref MemorySpan span, int value)
        {
            if (value > Int24_Max)
            {
                throw new OverflowException("Not Int24 Value!");
            }

            byte[] bytes = new byte[]
            {
                ((byte) (value >> 16)),
                ((byte) (value >> 8)),
                ((byte)value)
            };

            span.WriteBytes(bytes);
        }

        public static int ReadLTriad(ref MemorySpan span)
        {
            byte[] bytes = span.ReadBytes(3);

            return (bytes[0] | bytes[1] << 8 | bytes[2] << 16);
        }

        public static void WriteLTriad(ref MemorySpan span, int value)
        {
            if (value > Int24_Max)
            {
                throw new OverflowException("Not Int24 Value!");
            }

            byte[] bytes = new byte[3]
            {
                (byte) value,
                ((byte) (value >> 8)),
                ((byte) (value >> 16))
            };

            span.WriteBytes(bytes);
        }

        public static int ReadInt(ref MemorySpan span)
        {
            byte[] bytes = span.ReadBytes(4);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            return BitConverter.ToInt32(bytes, 0);
        }

        public static void WriteInt(ref MemorySpan span, int value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            span.WriteBytes(bytes);
        }

        public static uint ReadUInt(ref MemorySpan span)
        {
            byte[] bytes = span.ReadBytes(4);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            return BitConverter.ToUInt32(bytes, 0);
        }

        public static void WriteUInt(ref MemorySpan span, uint value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            span.WriteBytes(bytes);
        }

        public static uint ReadLInt(ref MemorySpan span)
        {
            byte[] bytes = span.ReadBytes(4);

            if (!BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            return BitConverter.ToUInt32(bytes, 0);
        }

        public static void WriteLInt(ref MemorySpan span, uint value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            if (!BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            span.WriteBytes(bytes);
        }

        public static long ReadLong(ref MemorySpan span)
        {
            byte[] bytes = span.ReadBytes(8);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            return BitConverter.ToInt64(bytes, 0);
        }

        public static void WriteLong(ref MemorySpan span, long value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            span.WriteBytes(bytes);
        }

        public static ulong ReadULong(ref MemorySpan span)
        {
            byte[] bytes = span.ReadBytes(8);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            return BitConverter.ToUInt64(bytes, 0);
        }

        public static void WriteULong(ref MemorySpan span, ulong value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            span.WriteBytes(bytes);
        }

        public static ulong ReadLLong(ref MemorySpan span)
        {
            byte[] bytes = span.ReadBytes(8);

            if (!BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            return BitConverter.ToUInt64(bytes, 0);
        }

        public static void WriteLLong(ref MemorySpan span, ulong value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            if (!BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            span.WriteBytes(bytes);
        }

        public static int ReadVarInt(ref MemorySpan span)
        {
            return VarInt.ReadInt32(ref span);
        }

        public static void WriteVarInt(ref MemorySpan span, int value)
        {
            VarInt.WriteInt32(ref span, value);
        }

        public static uint ReadUVarInt(ref MemorySpan span)
        {
            return VarInt.ReadUInt32(ref span);
        }

        public static void WriteUVarInt(ref MemorySpan span, uint value)
        {
            VarInt.WriteUInt32(ref span, value);
        }

        public static int ReadSVarInt(ref MemorySpan span)
        {
            return VarInt.ReadSInt32(ref span);
        }

        public static void WriteSVarInt(ref MemorySpan span, int value)
        {
            VarInt.WriteSInt32(ref span, value);
        }

        public static long ReadVarLong(ref MemorySpan span)
        {
            return VarInt.ReadInt64(ref span);
        }

        public static void WriteVarLong(ref MemorySpan span, long value)
        {
            VarInt.WriteInt64(ref span, value);
        }

        public static ulong ReadUVarLong(ref MemorySpan span)
        {
            return VarInt.ReadUInt64(ref span);
        }

        public static void WriteUVarLong(ref MemorySpan span, ulong value)
        {
            VarInt.WriteUInt64(ref span, value);
        }

        public static long ReadSVarLong(ref MemorySpan span)
        {
            return VarInt.ReadSInt64(ref span);
        }

        public static void WriteSVarLong(ref MemorySpan span, long value)
        {
            VarInt.WriteSInt64(ref span, value);
        }

        public static float ReadFloat(ref MemorySpan span)
        {
            byte[] bytes = span.ReadBytes(4);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            return BitConverter.ToSingle(bytes, 0);
        }

        public static void WriteFloat(ref MemorySpan span, float value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            span.WriteBytes(bytes);
        }

        public static float ReadLFloat(ref MemorySpan span)
        {
            byte[] bytes = span.ReadBytes(4);

            if (!BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            return BitConverter.ToSingle(bytes, 0);
        }

        public static void WriteLFloat(ref MemorySpan span, float value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            if (!BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            span.WriteBytes(bytes);
        }

        public static double ReadDouble(ref MemorySpan span)
        {
            byte[] bytes = span.ReadBytes(8);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            return BitConverter.ToDouble(bytes, 0);
        }

        public static void WriteDouble(ref MemorySpan span, double value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            span.WriteBytes(bytes);
        }

        public static double ReadLDouble(ref MemorySpan span)
        {
            byte[] bytes = span.ReadBytes(8);

            if (!BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            return BitConverter.ToDouble(bytes, 0);
        }

        public static void WriteLDouble(ref MemorySpan span, double value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            if (!BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            span.WriteBytes(bytes);
        }

        public static string ReadFixedString(ref MemorySpan span)
        {
            short len = ReadShort(ref span);
            if (len <= 0)
                return "";

            byte[] b = ReadBytes(ref span, len);
            return Encoding.UTF8.GetString(b);
        }

        public static void WriteFixedString(ref MemorySpan span, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                WriteShort(ref span, 0);
                return;
            }

            WriteShort(ref span, (short) value.Length);
            WriteBytes(ref span, Encoding.UTF8.GetBytes(value));
        }

        public static string ReadString(ref MemorySpan span)
        {
            uint len = ReadUVarInt(ref span);
            if (len == 0)
                return "";

            byte[] b = ReadBytes(ref span, (int) len);
            return Encoding.UTF8.GetString(b);
        }

        public static void WriteString(ref MemorySpan span, string value)
        {
            byte[] buf = Encoding.UTF8.GetBytes(value);
            if (string.IsNullOrEmpty(value))
            {
                WriteUVarInt(ref span, 0);
                return;
            }
            WriteUVarInt(ref span, (uint) buf.Length);
            WriteBytes(ref span, buf);
        }

        public static byte[] ReadBytes(ref MemorySpan span, int offset, int length)
        {
            return span.ReadBytes(offset, length);
        }

        public static byte[] ReadBytes(ref MemorySpan span, int length)
        {
            return span.ReadBytes(length);
        }

        public static byte[] ReadBytes(ref MemorySpan span)
        {
            return span.ReadBytes();
        }

        public static void WriteBytes(ref MemorySpan span, byte[] value)
        {
            span.WriteBytes(value);
        }

        public static byte[][] SplitBytes(MemorySpan span, int length)
        {
            List<byte[]> splits = new List<byte[]>();
            while (span.Offset < span.Length)
            {
                if ((span.Length - span.Offset) >= length)
                {
                    splits.Add(span.ReadBytes(length));
                }
                else
                {
                    splits.Add(span.ReadBytes());
                }
            }

            return splits.ToArray();
        }
    }
}
