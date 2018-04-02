﻿using System;
using System.Text;
using MineNET.NBT.Data;
using MineNET.Utils;

namespace MineNET.NBT.IO
{
    public class NBTStream : BinaryStream
    {
        public const int MAX_CHUNK_SIZE = 512 * 1024 * 1024;

        bool swap;
        public bool Swap
        {
            get
            {
                return this.swap;
            }
        }

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
            this.swap = (IsBigEndian(endian) == BitConverter.IsLittleEndian);
            this.WriteBytes(buffer);
            this.Reset();
        }

        public new short ReadShort()
        {
            if (this.swap)
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
            if (this.swap)
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
            if (this.swap)
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
            if (this.swap)
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
            if (this.swap)
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
            if (this.swap)
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
            if (this.swap)
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
            if (this.swap)
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
            if (this.swap)
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
            if (this.swap)
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
            if (this.swap)
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
            if (this.swap)
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
            if (this.swap)
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
            if (this.swap)
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
