using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

using MineNET.Values;

namespace MineNET.Utils
{
    public static class Binary
    {
        public static bool ReadBoolean(Stream buffer)
        {
            return buffer.ReadByte() < 1;
        }

        public static void PutBoolean(Stream buffer, bool value)
        {
            buffer.WriteByte((byte)(value ? 0x00 : 0x01));
        }

        public static byte ReadByte(Stream buffer)
        {
            byte b = (byte)buffer.ReadByte();
            return b;
        }

        public static void PutByte(Stream buffer, byte value)
        {
            buffer.WriteByte(value);
        }

        public static sbyte ReadSByte(Stream buffer)
        {
            var b = (sbyte)buffer.ReadByte();
            return b;
        }

        public static void PutSByte(Stream buffer, sbyte value)
        {
            buffer.WriteByte((byte)value);
        }

        public static short ReadShort(Stream buffer)
        {
            int b1 = buffer.ReadByte();
            int b2 = buffer.ReadByte();
            return (short)(b1 | b2 << 8);
        }

        public static void PutShort(Stream buffer, short value)
        {
            buffer.WriteByte((byte)value);
            buffer.WriteByte((byte)(value >> 8));
        }

        public static ushort ReadUShort(Stream buffer)
        {
            var b1 = buffer.ReadByte();
            var b2 = buffer.ReadByte();
            return (ushort)(b1 | b2 << 8);
        }

        public static void PutUShort(Stream buffer, ushort value)
        {
            buffer.WriteByte((byte)value);
            buffer.WriteByte((byte)(value >> 8));
        }

        public static ushort ReadLShort(Stream buffer)
        {
            int b1 = buffer.ReadByte();
            int b2 = buffer.ReadByte();
            return (ushort)(b1 << 8 | b2);
        }

        public static void PutLShort(Stream buffer, ushort value)
        {
            buffer.WriteByte((byte)(value >> 8));
            buffer.WriteByte((byte)value);
        }

        public static Int24 ReadLTriad(Stream buffer)
        {
            var b1 = buffer.ReadByte();
            var b2 = buffer.ReadByte();
            var b3 = buffer.ReadByte();
            return new Int24(new byte[3] 
            {
                (byte)b1,
                (byte)b2,
                (byte)b3
            });
        }

        public static void PutLTriad(Stream buffer, Int24 value)
        {
            byte[] bytes = value.ToBytes();
            buffer.WriteByte(bytes[0]);
            buffer.WriteByte(bytes[1]);
            buffer.WriteByte(bytes[2]);
        }

        public static Int24 ReadTriad(Stream buffer)
        {
            var b1 = buffer.ReadByte();
            var b2 = buffer.ReadByte();
            var b3 = buffer.ReadByte();
            return new Int24(new byte[3]
            {
                (byte)b3,
                (byte)b2,
                (byte)b1
            });
        }

        public static void PutTriad(Stream buffer, Int24 value)
        {
            byte[] r = value.ToBytes();
            buffer.WriteByte(r[2]);
            buffer.WriteByte(r[1]);
            buffer.WriteByte(r[0]);
        }

        public static int ReadInt(Stream buffer)
        {
            var b1 = buffer.ReadByte();
            var b2 = buffer.ReadByte();
            var b3 = buffer.ReadByte();
            var b4 = buffer.ReadByte();
            return (b1 | b2 << 8 | b3 << 16 | b4 << 24);
        }

        public static void PutInt(Stream buffer, int value)
        {
            buffer.WriteByte((byte)value);
            buffer.WriteByte((byte)(value >> 8));
            buffer.WriteByte((byte)(value >> 16));
            buffer.WriteByte((byte)(value >> 24));
        }

        public static uint ReadUInt(Stream buffer)
        {
            var b1 = buffer.ReadByte();
            var b2 = buffer.ReadByte();
            var b3 = buffer.ReadByte();
            var b4 = buffer.ReadByte();
            return (uint)(b1 | b2 << 8 | b3 << 16 | b4 << 24);
        }

        public static void PutUInt(Stream buffer, uint value)
        {
            buffer.WriteByte((byte)value);
            buffer.WriteByte((byte)(value >> 8));
            buffer.WriteByte((byte)(value >> 16));
            buffer.WriteByte((byte)(value >> 24));
        }

        public static uint ReadLInt(Stream buffer)
        {
            var b1 = buffer.ReadByte();
            var b2 = buffer.ReadByte();
            var b3 = buffer.ReadByte();
            var b4 = buffer.ReadByte();
            return (uint)(b1 << 24 | b2 << 16 | b3 << 8 | b4);
        }

        public static void PutLInt(Stream buffer, uint value)
        {
            buffer.WriteByte((byte)(value >> 24));
            buffer.WriteByte((byte)(value >> 16));
            buffer.WriteByte((byte)(value >> 8));
            buffer.WriteByte((byte)value);
        }

        public static long ReadLong(Stream buffer)
        {
            var b1 = buffer.ReadByte();
            var b2 = buffer.ReadByte();
            var b3 = buffer.ReadByte();
            var b4 = buffer.ReadByte();
            var b5 = buffer.ReadByte();
            var b6 = buffer.ReadByte();
            var b7 = buffer.ReadByte();
            var b8 = buffer.ReadByte();
            return (b1 | b2 << 8 | b3 << 16 | b4 << 24 | b1 << 32 | b2 << 40 | b3 << 48 | b4 << 56);
        }

        public static void PutLong(Stream buffer, long value)
        {
            buffer.WriteByte((byte)value);
            buffer.WriteByte((byte)(value >> 8));
            buffer.WriteByte((byte)(value >> 16));
            buffer.WriteByte((byte)(value >> 24));
            buffer.WriteByte((byte)(value >> 32));
            buffer.WriteByte((byte)(value >> 40));
            buffer.WriteByte((byte)(value >> 48));
            buffer.WriteByte((byte)(value >> 56));
        }

        public static ulong ReadULong(Stream buffer)
        {
            var b1 = buffer.ReadByte();
            var b2 = buffer.ReadByte();
            var b3 = buffer.ReadByte();
            var b4 = buffer.ReadByte();
            var b5 = buffer.ReadByte();
            var b6 = buffer.ReadByte();
            var b7 = buffer.ReadByte();
            var b8 = buffer.ReadByte();
            return (ulong)(b1 | b2 << 8 | b3 << 16 | b4 << 24 | b1 << 32 | b2 << 40 | b3 << 48 | b4 << 56);
        }

        public static void PutULong(Stream buffer, ulong value)
        {
            buffer.WriteByte((byte)value);
            buffer.WriteByte((byte)(value >> 8));
            buffer.WriteByte((byte)(value >> 16));
            buffer.WriteByte((byte)(value >> 24));
            buffer.WriteByte((byte)(value >> 32));
            buffer.WriteByte((byte)(value >> 40));
            buffer.WriteByte((byte)(value >> 48));
            buffer.WriteByte((byte)(value >> 56));
        }

        public static int ReadVarInt(Stream buffer)
        {
            return VarInt.ReadInt32(buffer);
        }

        public static void PutVarInt(Stream buffer, int value)
        {
            VarInt.WriteInt32(buffer, value);
        }

        public static uint ReadVarUInt(Stream buffer)
        {
            return VarInt.ReadUInt32(buffer);
        }

        public static void PutVarUInt(Stream buffer, uint value)
        {
            VarInt.WriteUInt32(buffer, value);
        }

        public static int ReadVarSInt(Stream buffer)
        {
            return VarInt.ReadSInt32(buffer);
        }

        public static void PutVarSInt(Stream buffer, int value)
        {
            VarInt.WriteSInt32(buffer, value);
        }

        public static long ReadVarLong(Stream buffer)
        {
            return VarInt.ReadInt64(buffer);
        }

        public static void PutVarLong(Stream buffer, long value)
        {
            VarInt.WriteInt64(buffer, value);
        }

        public static ulong ReadVarULong(Stream buffer)
        {
            return VarInt.ReadUInt64(buffer);
        }

        public static void PutVarULong(Stream buffer, ulong value)
        {
            VarInt.WriteUInt64(buffer, value);
        }

        public static long ReadVarSLong(Stream buffer)
        {
            return VarInt.ReadSInt64(buffer);
        }

        public static void PutVarSLong(Stream buffer, long value)
        {
            VarInt.WriteSInt64(buffer, value);
        }

        public static float ReadFloat(Stream buffer)
        {
            var b1 = buffer.ReadByte();
            var b2 = buffer.ReadByte();
            var b3 = buffer.ReadByte();
            var b4 = buffer.ReadByte();
            return BitConverter.ToSingle(new byte[4]
            {
                (byte)b1,
                (byte)b2,
                (byte)b3,
                (byte)b4
            }, 0);
        }

        public static void PutFloat(Stream buffer, float value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            buffer.WriteByte(bytes[0]);
            buffer.WriteByte(bytes[1]);
            buffer.WriteByte(bytes[2]);
            buffer.WriteByte(bytes[3]);
        }

        public static double ReadDouble(Stream buffer)
        {
            var b1 = buffer.ReadByte();
            var b2 = buffer.ReadByte();
            var b3 = buffer.ReadByte();
            var b4 = buffer.ReadByte();
            var b5 = buffer.ReadByte();
            var b6 = buffer.ReadByte();
            var b7 = buffer.ReadByte();
            var b8 = buffer.ReadByte();
            return BitConverter.ToDouble(new byte[8]
            {
                (byte)b1,
                (byte)b2,
                (byte)b3,
                (byte)b4,
                (byte)b5,
                (byte)b6,
                (byte)b7,
                (byte)b8
            }, 0);
        }

        public static void PutDouble(Stream buffer, double value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            buffer.WriteByte(bytes[0]);
            buffer.WriteByte(bytes[1]);
            buffer.WriteByte(bytes[2]);
            buffer.WriteByte(bytes[3]);
            buffer.WriteByte(bytes[4]);
            buffer.WriteByte(bytes[5]);
            buffer.WriteByte(bytes[6]);
            buffer.WriteByte(bytes[7]);
        }

        public static byte[] GetBytes(MemoryStream buffer, int start, int len)
        {
            List<byte> result = new List<byte>();
            byte[] raw = buffer.GetBuffer();
            for (int i = start; i < len; ++i)
            {
                if (i > buffer.Length - 1) break;
                result.Add(raw[i]);
            }
            buffer.Position += len;
            return result.ToArray();
        }

        public static byte[] GetBytes(MemoryStream buffer, int start)
        {
            List<byte> result = new List<byte>();
            byte[] raw = buffer.ToArray();
            for (int i = start; i < (buffer.Length - start); ++i)
            {
                if (i > buffer.Length) break;
                result.Add(raw[i]);
            }
            buffer.Position += buffer.Length - start;
            return result.ToArray();
        }

        public static byte[][] SplitBytes(MemoryStream buffer, int len)
        {
            byte[][] result = new byte[(buffer.Position + len - 1) / len][];
            int c = 0;
            for (int i = 0; i < buffer.Length; i += len)
            {
                if ((buffer.Length - i) > len)
                {
                    result[c] = GetBytes(buffer, i, i + len);
                }
                else
                {
                    result[c] = GetBytes(buffer, i, (int)buffer.Length);
                }
                c++;
            }
            return result;
        }
    }
}
