using System;
using System.IO;
using System.Text;
using MineNET.NBT.Data;
using MineNET.Utils;

namespace MineNET.NBT.IO
{
    public class NBTStream : MemoryStream
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
            this.Write(buffer, 0, buffer.Length);
            this.Position = 0;
        }

        public new byte ReadByte()
        {
            return Binary.ReadByte(this);
        }

        public new void WriteByte(byte value)
        {
            Binary.WriteByte(this, value);
        }

        public short ReadShort()
        {
            if (this.swap)
            {
                return (short) Binary.ReadLShort(this);
            }
            else
            {
                return Binary.ReadShort(this);
            }
        }

        public void WriteShort(short value)
        {
            if (this.swap)
            {
                Binary.WriteLShort(this, (ushort) value);
            }
            else
            {
                Binary.WriteShort(this, value);
            }
        }

        public int ReadInt()
        {
            if (this.swap)
            {
                return (int) Binary.ReadLInt(this);
            }
            else
            {
                return Binary.ReadInt(this);
            }
        }

        public void WriteInt(int value)
        {
            if (this.swap)
            {
                Binary.WriteLInt(this, (uint) value);
            }
            else
            {
                Binary.WriteInt(this, value);
            }
        }

        public long ReadLong()
        {
            if (this.swap)
            {
                return (long) Binary.ReadLLong(this);
            }
            else
            {
                return Binary.ReadLong(this);
            }
        }

        public void WriteLong(long value)
        {
            if (this.swap)
            {
                Binary.WriteLLong(this, (ulong) value);
            }
            else
            {
                Binary.WriteLong(this, value);
            }
        }

        public float ReadFloat()
        {
            if (this.swap)
            {
                return Binary.ReadLFloat(this);
            }
            else
            {
                return Binary.ReadShort(this);
            }
        }

        public void WriteFloat(float value)
        {
            if (this.swap)
            {
                Binary.WriteLFloat(this, value);
            }
            else
            {
                Binary.WriteFloat(this, value);
            }
        }

        public double ReadDouble()
        {
            if (this.swap)
            {
                return Binary.ReadLDouble(this);
            }
            else
            {
                return Binary.ReadDouble(this);
            }
        }

        public void WriteDouble(double value)
        {
            if (this.swap)
            {
                Binary.WriteLDouble(this, value);
            }
            else
            {
                Binary.WriteDouble(this, value);
            }
        }

        public string ReadString()
        {
            short len = ReadShort();
            byte[] buffer = Binary.ReadBytes(this, (int) this.Position, len);
            return this.utf8.GetString(buffer);
        }

        public void WriteString(string value)
        {
            WriteShort((short) value.Length);
            Binary.WriteBytes(this, this.utf8.GetBytes(value));
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
