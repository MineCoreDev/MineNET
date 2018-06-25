using MineNET.NBT.Data;
using MineNET.Utils;
using System;
using System.Text;

namespace MineNET.NBT.IO
{
    public class NBTStream : BinaryStream
    {
        public const int MAX_CHUNK_SIZE = 512 * 1024 * 1024;

        public bool Swap { get; set; }

        public bool Network { get; set; }

        Encoding utf8 = new UTF8Encoding(false, false);

        public NBTStream(NBTEndian endian = NBTEndian.LITTLE_ENDIAN) : this(new byte[0], endian)
        {

        }

        public NBTStream(byte[] buffer) : this(buffer, NBTEndian.LITTLE_ENDIAN)
        {

        }

        public NBTStream(byte[] buffer, NBTEndian endian)
        {
            this.Swap = (IsBigEndian(endian) == BitConverter.IsLittleEndian);
            this.WriteBytes(buffer);
            this.Reset();
        }

        public new short ReadShort()
        {
            if (this.Swap)
            {
                return (short) this.ReadLShort();
            }
            else
            {
                return base.ReadShort();
            }
        }

        public new void WriteShort(short value)
        {
            if (this.Swap)
            {
                this.WriteLShort((ushort) value);
            }
            else
            {
                base.WriteShort(value);
            }
        }

        public new int ReadInt()
        {
            if (this.Network)
            {
                return this.ReadSVarInt();
            }
            if (this.Swap)
            {
                return (int) this.ReadLInt();
            }
            else
            {
                return base.ReadInt();
            }
        }

        public new void WriteInt(int value)
        {
            if (this.Network)
            {
                this.WriteSVarInt(value);
                return;
            }
            if (this.Swap)
            {
                this.WriteLInt((uint) value);
            }
            else
            {
                base.WriteInt(value);
            }
        }

        public new long ReadLong()
        {
            if (this.Network)
            {
                return this.ReadSVarLong();
            }
            if (this.Swap)
            {
                return (long) this.ReadLLong();
            }
            else
            {
                return base.ReadLong();
            }
        }

        public new void WriteLong(long value)
        {
            if (this.Network)
            {
                this.WriteSVarLong(value);
                return;
            }
            if (this.Swap)
            {
                this.WriteLLong((ulong) value);
            }
            else
            {
                base.WriteLong(value);
            }
        }

        public new float ReadFloat()
        {
            if (this.Swap)
            {
                return this.ReadLFloat();
            }
            else
            {
                return base.ReadFloat();
            }
        }

        public new void WriteFloat(float value)
        {
            if (this.Swap)
            {
                this.WriteLFloat(value);
            }
            else
            {
                base.WriteFloat(value);
            }
        }

        public new double ReadDouble()
        {
            if (this.Swap)
            {
                return this.ReadLDouble();
            }
            else
            {
                return base.ReadDouble();
            }
        }

        public new void WriteDouble(double value)
        {
            if (this.Swap)
            {
                this.WriteLDouble(value);
            }
            else
            {
                base.WriteDouble(value);
            }
        }

        public new int ReadSVarInt()
        {
            if (this.Swap)
            {
                return ~base.ReadSVarInt();
            }
            else
            {
                return base.ReadSVarInt();
            }
        }

        public new void WriteSVarInt(int value)
        {
            if (this.Swap)
            {
                base.WriteSVarInt(~value);
            }
            else
            {
                base.WriteSVarInt(value);
            }
        }

        public new long ReadSVarLong()
        {
            if (this.Swap)
            {
                return ~base.ReadSVarLong();
            }
            else
            {
                return base.ReadSVarLong();
            }
        }

        public new void WriteSVarLong(long value)
        {
            if (this.Swap)
            {
                base.WriteSVarLong(~value);
            }
            else
            {
                base.WriteSVarLong(value);
            }
        }

        public new string ReadString()
        {
            int len = 0;
            if (this.Network)
            {
                len = this.ReadSVarInt();
            }
            else
            {
                len = this.ReadShort();
            }

            return Encoding.UTF8.GetString(this.ReadBytes(len));
        }

        public new void WriteString(string value)
        {
            if (value == null)
            {
                if (this.Network)
                {
                    this.WriteSVarInt(0);
                }
                else
                {
                    this.WriteShort(0);
                }
                return;
            }

            byte[] buffer = Encoding.UTF8.GetBytes(value);
            if (this.Network)
            {
                this.WriteSVarInt((int) buffer.Length);
            }
            else
            {
                this.WriteShort((short) buffer.Length);
            }
            this.WriteBytes(buffer);
        }

        static bool IsBigEndian(NBTEndian e)
        {
            if (e == NBTEndian.BIG_ENDIAN)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
