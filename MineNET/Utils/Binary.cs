using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MineNET.Utils
{
    public sealed class Binary
    {
        public const int Int24_Max = 16777215;

        public static bool ReadBool(Stream stream)
        {
            List<byte> bytes = new List<byte>();

            bytes.Add((byte)stream.ReadByte());

            return BitConverter.ToBoolean(bytes.ToArray(), 0);
        }

        public static void WriteBool(Stream stream, bool value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            stream.WriteByte(bytes[0]);
        }

        public static byte ReadByte(Stream stream)
        {
            return (byte)stream.ReadByte();
        }

        public static void WriteByte(Stream stream, byte value)
        {
            stream.WriteByte(value);
        }

        public static sbyte ReadSByte(Stream stream)
        {
            return (sbyte)stream.ReadByte();
        }

        public static void WriteSByte(Stream stream, sbyte value)
        {
            stream.WriteByte((byte)value);
        }

        public static short ReadShort(Stream stream)
        {
            List<byte> bytes = new List<byte>();

            bytes.Add((byte)stream.ReadByte());
            bytes.Add((byte)stream.ReadByte());

            return BitConverter.ToInt16(bytes.ToArray(), 0);
        }

        public static void WriteShort(Stream stream, short value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            stream.WriteByte(bytes[0]);
            stream.WriteByte(bytes[1]);
        }

        public static ushort ReadUShort(Stream stream)
        {
            List<byte> bytes = new List<byte>();

            bytes.Add((byte)stream.ReadByte());
            bytes.Add((byte)stream.ReadByte());

            return BitConverter.ToUInt16(bytes.ToArray(), 0);
        }

        public static void WriteUShort(Stream stream, ushort value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            stream.WriteByte(bytes[0]);
            stream.WriteByte(bytes[1]);
        }

        public static ushort ReadLShort(Stream stream)
        {
            List<byte> bytes = new List<byte>();

            bytes.Add((byte)stream.ReadByte());
            bytes.Add((byte)stream.ReadByte());

            bytes.Reverse();

            return BitConverter.ToUInt16(bytes.ToArray(), 0);
        }

        public static void WriteLShort(Stream stream, ushort value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            stream.WriteByte(bytes[1]);
            stream.WriteByte(bytes[0]);
        }

        public static int ReadTriad(Stream stream)
        {
            int b0 = stream.ReadByte();
            int b1 = stream.ReadByte();
            int b2 = stream.ReadByte();

            return (b0 << 16 | b1 << 8 | b2);
        }

        public static void WriteTriad(Stream stream, int value)
        {
            if (value > Int24_Max)
            {
                throw new OverflowException("Not Int24 Value!");
            }
            stream.WriteByte((byte)(value >> 16));
            stream.WriteByte((byte)(value >> 8));
            stream.WriteByte((byte)value);
        }

        public static int ReadLTriad(Stream stream)
        {
            int b0 = stream.ReadByte();
            int b1 = stream.ReadByte();
            int b2 = stream.ReadByte();

            return (b0 | b1 << 8 | b2 << 16);
        }

        public static void WriteLTriad(Stream stream, int value)
        {
            if (value > Int24_Max)
            {
                throw new OverflowException("Not Int24 Value!");
            }
            stream.WriteByte((byte)value);
            stream.WriteByte((byte)(value >> 8));
            stream.WriteByte((byte)(value >> 16));
        }

        //TODO: Endian Bug Fix...
        /*public static int ReadTriad(Stream stream)
        {
            if(BitConverter.IsLittleEndian)
            {
                return _ReadLTriad(stream);
            }
            else
            {
                return _WriteTriad(stream);
            }
        }*/

        public static int ReadInt(Stream stream)
        {
            List<byte> bytes = new List<byte>();

            bytes.Add((byte)stream.ReadByte());
            bytes.Add((byte)stream.ReadByte());
            bytes.Add((byte)stream.ReadByte());
            bytes.Add((byte)stream.ReadByte());

            return BitConverter.ToInt32(bytes.ToArray(), 0);
        }

        public static void WriteInt(Stream stream, int value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            stream.WriteByte(bytes[0]);
            stream.WriteByte(bytes[1]);
            stream.WriteByte(bytes[2]);
            stream.WriteByte(bytes[3]);
        }

        public static uint ReadUInt(Stream stream)
        {
            List<byte> bytes = new List<byte>();

            bytes.Add((byte)stream.ReadByte());
            bytes.Add((byte)stream.ReadByte());
            bytes.Add((byte)stream.ReadByte());
            bytes.Add((byte)stream.ReadByte());

            return BitConverter.ToUInt32(bytes.ToArray(), 0);
        }

        public static void WriteUInt(Stream stream, uint value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            stream.WriteByte(bytes[0]);
            stream.WriteByte(bytes[1]);
            stream.WriteByte(bytes[2]);
            stream.WriteByte(bytes[3]);
        }

        public static uint ReadLInt(Stream stream)
        {
            List<byte> bytes = new List<byte>();

            bytes.Add((byte)stream.ReadByte());
            bytes.Add((byte)stream.ReadByte());
            bytes.Add((byte)stream.ReadByte());
            bytes.Add((byte)stream.ReadByte());

            bytes.Reverse();

            return BitConverter.ToUInt32(bytes.ToArray(), 0);
        }

        public static void WriteLInt(Stream stream, uint value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            stream.WriteByte(bytes[3]);
            stream.WriteByte(bytes[2]);
            stream.WriteByte(bytes[1]);
            stream.WriteByte(bytes[0]);
        }

        public static long ReadLong(Stream stream)
        {
            List<byte> bytes = new List<byte>();

            bytes.Add((byte)stream.ReadByte());
            bytes.Add((byte)stream.ReadByte());
            bytes.Add((byte)stream.ReadByte());
            bytes.Add((byte)stream.ReadByte());
            bytes.Add((byte)stream.ReadByte());
            bytes.Add((byte)stream.ReadByte());
            bytes.Add((byte)stream.ReadByte());
            bytes.Add((byte)stream.ReadByte());

            return BitConverter.ToInt64(bytes.ToArray(), 0);
        }

        public static void WriteLong(Stream stream, long value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            stream.WriteByte(bytes[0]);
            stream.WriteByte(bytes[1]);
            stream.WriteByte(bytes[2]);
            stream.WriteByte(bytes[3]);
            stream.WriteByte(bytes[4]);
            stream.WriteByte(bytes[5]);
            stream.WriteByte(bytes[6]);
            stream.WriteByte(bytes[7]);
        }

        public static ulong ReadULong(Stream stream)
        {
            List<byte> bytes = new List<byte>();

            bytes.Add((byte)stream.ReadByte());
            bytes.Add((byte)stream.ReadByte());
            bytes.Add((byte)stream.ReadByte());
            bytes.Add((byte)stream.ReadByte());
            bytes.Add((byte)stream.ReadByte());
            bytes.Add((byte)stream.ReadByte());
            bytes.Add((byte)stream.ReadByte());
            bytes.Add((byte)stream.ReadByte());

            return BitConverter.ToUInt64(bytes.ToArray(), 0);
        }

        public static void WriteULong(Stream stream, ulong value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            stream.WriteByte(bytes[0]);
            stream.WriteByte(bytes[1]);
            stream.WriteByte(bytes[2]);
            stream.WriteByte(bytes[3]);
            stream.WriteByte(bytes[4]);
            stream.WriteByte(bytes[5]);
            stream.WriteByte(bytes[6]);
            stream.WriteByte(bytes[7]);
        }

        public static ulong ReadLLong(Stream stream)
        {
            List<byte> bytes = new List<byte>();

            bytes.Add((byte)stream.ReadByte());
            bytes.Add((byte)stream.ReadByte());
            bytes.Add((byte)stream.ReadByte());
            bytes.Add((byte)stream.ReadByte());

            bytes.Reverse();

            return BitConverter.ToUInt64(bytes.ToArray(), 0);
        }

        public static void WriteLLong(Stream stream, ulong value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            stream.WriteByte(bytes[7]);
            stream.WriteByte(bytes[6]);
            stream.WriteByte(bytes[5]);
            stream.WriteByte(bytes[4]);
            stream.WriteByte(bytes[3]);
            stream.WriteByte(bytes[2]);
            stream.WriteByte(bytes[1]);
            stream.WriteByte(bytes[0]);
        }

        public static int ReadVarInt(Stream stream)
        {
            return VarInt.ReadInt32(stream);
        }

        public static void WriteVarInt(Stream stream, int value)
        {
            VarInt.WriteInt32(stream, value);
        }

        public static uint ReadUVarInt(Stream stream)
        {
            return VarInt.ReadUInt32(stream);
        }

        public static void WriteUVarInt(Stream stream, uint value)
        {
            VarInt.WriteUInt32(stream, value);
        }

        public static long ReadVarLong(Stream stream)
        {
            return VarInt.ReadInt64(stream);
        }

        public static void WriteVarLong(Stream stream, long value)
        {
            VarInt.WriteInt64(stream, value);
        }

        public static ulong ReadUVarLong(Stream stream)
        {
            return VarInt.ReadUInt64(stream);
        }

        public static void WriteUVarLong(Stream stream, ulong value)
        {
            VarInt.WriteUInt64(stream, value);
        }

        public static float ReadFloat(Stream stream)
        {
            List<byte> bytes = new List<byte>();

            bytes.Add((byte)stream.ReadByte());
            bytes.Add((byte)stream.ReadByte());
            bytes.Add((byte)stream.ReadByte());
            bytes.Add((byte)stream.ReadByte());

            return BitConverter.ToSingle(bytes.ToArray(), 0);
        }

        public static void WriteFloat(Stream stream, float value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            stream.WriteByte(bytes[0]);
            stream.WriteByte(bytes[1]);
            stream.WriteByte(bytes[2]);
            stream.WriteByte(bytes[3]);
        }

        public static double ReadDouble(Stream stream)
        {
            List<byte> bytes = new List<byte>();

            bytes.Add((byte)stream.ReadByte());
            bytes.Add((byte)stream.ReadByte());
            bytes.Add((byte)stream.ReadByte());
            bytes.Add((byte)stream.ReadByte());
            bytes.Add((byte)stream.ReadByte());
            bytes.Add((byte)stream.ReadByte());
            bytes.Add((byte)stream.ReadByte());
            bytes.Add((byte)stream.ReadByte());

            return BitConverter.ToDouble(bytes.ToArray(), 0);
        }

        public static void WriteDouble(Stream stream, double value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            stream.WriteByte(bytes[0]);
            stream.WriteByte(bytes[1]);
            stream.WriteByte(bytes[2]);
            stream.WriteByte(bytes[3]);
            stream.WriteByte(bytes[4]);
            stream.WriteByte(bytes[5]);
            stream.WriteByte(bytes[6]);
            stream.WriteByte(bytes[7]);
        }

        public static string ReadFixedString(MemoryStream stream)
        {
            ushort len = ReadLShort(stream);
            if (len <= 0) return string.Empty;
            byte[] b = ReadBytes(stream, (int)stream.Position, len);
            Console.WriteLine(b.Length);
            return Encoding.UTF8.GetString(b);
        }

        public static void WriteFixedString(MemoryStream stream, string value)
        {
            WriteLShort(stream, (ushort)(value.Length));
            WriteBytes(stream, Encoding.UTF8.GetBytes(value));
        }

        public static byte[] ReadBytes(MemoryStream stream, int start, int length)
        {
            List<byte> result = new List<byte>();
            byte[] raw = stream.ToArray();
            for (int i = start; i < start + length; ++i)
            {
                result.Add(raw[i]);
            }
            stream.Position += length;
            return result.ToArray();
        }

        public static void WriteBytes(MemoryStream stream, byte[] value)
        {
            for (int i = 0; i < value.Length; ++i)
            {
                stream.WriteByte(value[i]);
            }
        }

        public static void Reset(Stream stream)
        {
            stream.Position = 0;
        }

        public static bool EndOfStream(Stream stream)
        {
            return stream.Position >= stream.Length;
        }
    }
}
