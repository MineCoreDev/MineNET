using System;
using System.Collections.Generic;
using System.Text;

namespace MineNET.Utils
{
    public sealed class Binary
    {
        public const int Int24_Max = 16777215;

        public static bool ReadBool(List<byte> buffer, int offset)
        {
            List<byte> bytes = new List<byte>();

            bytes.Add(buffer[offset]);

            return BitConverter.ToBoolean(bytes.ToArray(), 0);
        }

        public static void WriteBool(List<byte> buffer, bool value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            buffer.AddRange(bytes);
        }

        public static byte ReadByte(List<byte> buffer, int offset)
        {
            return buffer[offset];
        }

        public static void WriteByte(List<byte> buffer, byte value)
        {
            buffer.Add(value);
        }

        public static sbyte ReadSByte(List<byte> buffer, int offset)
        {
            return (sbyte) buffer[offset];
        }

        public static void WriteSByte(List<byte> buffer, sbyte value)
        {
            buffer.Add((byte) value);
        }

        public static short ReadShort(List<byte> buffer, int offset)
        {
            List<byte> bytes = new List<byte>();

            bytes.AddRange(buffer.GetRange(offset, 2));

            if (BitConverter.IsLittleEndian)
            {
                bytes.Reverse();
            }

            return BitConverter.ToInt16(bytes.ToArray(), 0);
        }

        public static void WriteShort(List<byte> buffer, short value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            buffer.AddRange(bytes);
        }

        public static ushort ReadUShort(List<byte> buffer, int offset)
        {
            List<byte> bytes = new List<byte>();

            bytes.AddRange(buffer.GetRange(offset, 2));

            if (BitConverter.IsLittleEndian)
            {
                bytes.Reverse();
            }

            return BitConverter.ToUInt16(bytes.ToArray(), 0);
        }

        public static void WriteUShort(List<byte> buffer, ushort value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            buffer.AddRange(bytes);
        }

        public static ushort ReadLShort(List<byte> buffer, int offset)
        {
            List<byte> bytes = new List<byte>();

            bytes.AddRange(buffer.GetRange(offset, 2));

            if (!BitConverter.IsLittleEndian)
            {
                bytes.Reverse();
            }

            return BitConverter.ToUInt16(bytes.ToArray(), 0);
        }

        public static void WriteLShort(List<byte> buffer, ushort value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            if (!BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            buffer.AddRange(bytes);
        }

        public static int ReadTriad(List<byte> buffer, int offset)
        {
            List<byte> bytes = new List<byte>();

            bytes.AddRange(buffer.GetRange(offset, 3));

            return (bytes[0] << 16 | bytes[1] << 8 | bytes[2]);
        }

        public static void WriteTriad(List<byte> buffer, int value)
        {
            if (value > Int24_Max)
            {
                throw new OverflowException("Not Int24 Value!");
            }

            List<byte> bytes = new List<byte>();

            bytes.Add((byte) (value >> 16));
            bytes.Add((byte) (value >> 8));
            bytes.Add((byte) value);

            buffer.AddRange(bytes);
        }

        public static int ReadLTriad(List<byte> buffer, int offset)
        {
            List<byte> bytes = new List<byte>();

            bytes.AddRange(buffer.GetRange(offset, 3));

            return (bytes[0] | bytes[1] << 8 | bytes[2] << 16);
        }

        public static void WriteLTriad(List<byte> buffer, int value)
        {
            if (value > Int24_Max)
            {
                throw new OverflowException("Not Int24 Value!");
            }

            List<byte> bytes = new List<byte>();

            bytes.Add((byte) value);
            bytes.Add((byte) (value >> 8));
            bytes.Add((byte) (value >> 16));

            buffer.AddRange(bytes);
        }

        public static int ReadInt(List<byte> buffer, int offset)
        {
            List<byte> bytes = new List<byte>();

            bytes.AddRange(buffer.GetRange(offset, 4));

            if (BitConverter.IsLittleEndian)
            {
                bytes.Reverse();
            }

            return BitConverter.ToInt32(bytes.ToArray(), 0);
        }

        public static void WriteInt(List<byte> buffer, int value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            buffer.AddRange(bytes);
        }

        public static uint ReadUInt(List<byte> buffer, int offset)
        {
            List<byte> bytes = new List<byte>();

            bytes.AddRange(buffer.GetRange(offset, 4));

            if (BitConverter.IsLittleEndian)
            {
                bytes.Reverse();
            }

            return BitConverter.ToUInt32(bytes.ToArray(), 0);
        }

        public static void WriteUInt(List<byte> buffer, uint value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            buffer.AddRange(bytes);
        }

        public static uint ReadLInt(List<byte> buffer, int offset)
        {
            List<byte> bytes = new List<byte>();

            bytes.AddRange(buffer.GetRange(offset, 4));

            if (!BitConverter.IsLittleEndian)
            {
                bytes.Reverse();
            }

            return BitConverter.ToUInt32(bytes.ToArray(), 0);
        }

        public static void WriteLInt(List<byte> buffer, uint value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            if (!BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            buffer.AddRange(bytes);
        }

        public static long ReadLong(List<byte> buffer, int offset)
        {
            List<byte> bytes = new List<byte>();

            bytes.AddRange(buffer.GetRange(offset, 8));

            if (BitConverter.IsLittleEndian)
            {
                bytes.Reverse();
            }

            return BitConverter.ToInt64(bytes.ToArray(), 0);
        }

        public static void WriteLong(List<byte> buffer, long value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            buffer.AddRange(bytes);
        }

        public static ulong ReadULong(List<byte> buffer, int offset)
        {
            List<byte> bytes = new List<byte>();

            bytes.AddRange(buffer.GetRange(offset, 8));

            if (BitConverter.IsLittleEndian)
            {
                bytes.Reverse();
            }

            return BitConverter.ToUInt64(bytes.ToArray(), 0);
        }

        public static void WriteULong(List<byte> buffer, ulong value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            buffer.AddRange(bytes);
        }

        public static ulong ReadLLong(List<byte> buffer, int offset)
        {
            List<byte> bytes = new List<byte>();

            bytes.AddRange(buffer.GetRange(offset, 8));

            if (!BitConverter.IsLittleEndian)
            {
                bytes.Reverse();
            }

            return BitConverter.ToUInt64(bytes.ToArray(), 0);
        }

        public static void WriteLLong(List<byte> buffer, ulong value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            if (!BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            buffer.AddRange(bytes);
        }

        public static int ReadVarInt(List<byte> buffer, ref int offset)
        {
            return VarInt.ReadInt32(buffer, ref offset);
        }

        public static void WriteVarInt(List<byte> buffer, int value, out int moveOffset)
        {
            VarInt.WriteInt32(buffer, value, out moveOffset);
        }

        public static uint ReadUVarInt(List<byte> buffer, ref int offset)
        {
            return VarInt.ReadUInt32(buffer, ref offset);
        }

        public static void WriteUVarInt(List<byte> buffer, uint value, out int moveOffset)
        {
            VarInt.WriteUInt32(buffer, value, out moveOffset);
        }

        public static int ReadSVarInt(List<byte> buffer, ref int offset)
        {
            return VarInt.ReadSInt32(buffer, ref offset);
        }

        public static void WriteSVarInt(List<byte> buffer, int value, out int moveOffset)
        {
            VarInt.WriteSInt32(buffer, value, out moveOffset);
        }

        public static long ReadVarLong(List<byte> buffer, ref int offset)
        {
            return VarInt.ReadInt64(buffer, ref offset);
        }

        public static void WriteVarLong(List<byte> buffer, long value, out int moveOffset)
        {
            VarInt.WriteInt64(buffer, value, out moveOffset);
        }

        public static ulong ReadUVarLong(List<byte> buffer, ref int offset)
        {
            return VarInt.ReadUInt64(buffer, ref offset);
        }

        public static void WriteUVarLong(List<byte> buffer, ulong value, out int moveOffset)
        {
            VarInt.WriteUInt64(buffer, value, out moveOffset);
        }

        public static long ReadSVarLong(List<byte> buffer, ref int offset)
        {
            return VarInt.ReadSInt64(buffer, ref offset);
        }

        public static void WriteSVarLong(List<byte> buffer, long value, out int moveOffset)
        {
            VarInt.WriteSInt64(buffer, value, out moveOffset);
        }

        public static float ReadFloat(List<byte> buffer, int offset)
        {
            List<byte> bytes = new List<byte>();

            bytes.AddRange(buffer.GetRange(offset, 4));

            if (BitConverter.IsLittleEndian)
            {
                bytes.Reverse();
            }

            return BitConverter.ToSingle(bytes.ToArray(), 0);
        }

        public static void WriteFloat(List<byte> buffer, float value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            buffer.AddRange(bytes);
        }

        public static float ReadLFloat(List<byte> buffer, int offset)
        {
            List<byte> bytes = new List<byte>();

            bytes.AddRange(buffer.GetRange(offset, 4));

            if (!BitConverter.IsLittleEndian)
            {
                bytes.Reverse();
            }

            return BitConverter.ToSingle(bytes.ToArray(), 0);
        }

        public static void WriteLFloat(List<byte> buffer, float value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            if (!BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            buffer.AddRange(bytes);
        }

        public static double ReadDouble(List<byte> buffer, int offset)
        {
            List<byte> bytes = new List<byte>();

            bytes.AddRange(buffer.GetRange(offset, 8));

            if (BitConverter.IsLittleEndian)
            {
                bytes.Reverse();
            }

            return BitConverter.ToDouble(bytes.ToArray(), 0);
        }

        public static void WriteDouble(List<byte> buffer, double value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            buffer.AddRange(bytes);
        }

        public static double ReadLDouble(List<byte> buffer, int offset)
        {
            List<byte> bytes = new List<byte>();

            bytes.AddRange(buffer.GetRange(offset, 8));

            if (!BitConverter.IsLittleEndian)
            {
                bytes.Reverse();
            }

            return BitConverter.ToDouble(bytes.ToArray(), 0);
        }

        public static void WriteLDouble(List<byte> buffer, double value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            if (!BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            buffer.AddRange(bytes);
        }

        public static string ReadFixedString(List<byte> buffer, int offset)
        {
            short len = ReadShort(buffer, offset);
            if (len <= 0)
                return string.Empty;

            byte[] b = ReadBytes(buffer, offset + 2, len);
            return Encoding.UTF8.GetString(b);
        }

        public static void WriteFixedString(List<byte> buffer, string value)
        {
            WriteShort(buffer, (short) value.Length);
            WriteBytes(buffer, Encoding.UTF8.GetBytes(value));
        }

        public static string ReadString(List<byte> buffer, ref int offset)
        {
            uint len = ReadUVarInt(buffer, ref offset);
            if (len <= 0)
                return string.Empty;

            byte[] b = ReadBytes(buffer, offset, (int) len);
            return Encoding.UTF8.GetString(b);
        }

        public static void WriteString(List<byte> buffer, string value, out int moveOffset)
        {
            WriteUVarInt(buffer, (uint) value.Length, out moveOffset);
            WriteBytes(buffer, Encoding.UTF8.GetBytes(value));
        }

        public static byte[] ReadBytes(List<byte> buffer, int start, int length)
        {
            List<byte> result = new List<byte>();
            byte[] raw = buffer.ToArray();
            for (int i = start; i < start + length; ++i)
            {
                result.Add(raw[i]);
            }
            return result.ToArray();
        }

        public static void WriteBytes(List<byte> buffer, byte[] value)
        {
            for (int i = 0; i < value.Length; ++i)
            {
                buffer.Add(value[i]);
            }
        }

        public static byte[][] SplitBytes(byte[] buffer, int length)
        {
            List<byte[]> splits = new List<byte[]>();
            int count = 0;
            while (count != buffer.Length)
            {
                List<byte> split = new List<byte>();
                for (int i = count; i < length; ++i)
                {
                    split.Add(buffer[i]);
                    count++;
                }
                splits.Add(split.ToArray());
            }

            return splits.ToArray();
        }
    }
}
