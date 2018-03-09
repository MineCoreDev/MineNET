using System;
using System.Collections.Generic;
using System.Net;

namespace MineNET.Utils
{
    public class BinaryStream : IDisposable
    {
        List<byte> buffer = new List<byte>();

        public int Offset { get; set; }

        public int Length
        {
            get
            {
                return this.buffer.Count;
            }
        }

        public bool EndOfStream
        {
            get
            {
                return this.Offset >= this.Length;
            }
        }

        #region Ctor Method

        public BinaryStream()
        {
            this.Reset();
            this.buffer = new List<byte>();
        }

        public BinaryStream(byte[] buffer, bool reset = true)
        {
            this.Reset();
            this.buffer = new List<byte>();
            this.WriteBytes(buffer);
            if (reset)
            {
                this.Reset();
            }
        }

        #endregion

        #region Public Method

        public bool ReadBoolean()
        {
            bool v = Binary.ReadBool(this.buffer, this.Offset);
            this.Offset += 1;
            return v;
        }

        public void WriteBoolean(bool value)
        {
            this.Offset += 1;
            Binary.WriteBool(this.buffer, value);
        }

        public byte ReadByte()
        {
            byte v = Binary.ReadByte(this.buffer, this.Offset);
            this.Offset += 1;
            return v;
        }

        public void WriteByte(byte value)
        {
            this.Offset += 1;
            Binary.WriteByte(this.buffer, value);
        }

        public sbyte ReadSByte()
        {
            sbyte v = Binary.ReadSByte(this.buffer, this.Offset);
            this.Offset += 1;
            return v;
        }

        public void WriteSByte(sbyte value)
        {
            this.Offset += 1;
            Binary.WriteSByte(this.buffer, value);
        }

        public short ReadShort()
        {
            short v = Binary.ReadShort(this.buffer, this.Offset);
            this.Offset += 2;
            return v;
        }

        public void WriteShort(short value)
        {
            this.Offset += 2;
            Binary.WriteShort(this.buffer, value);
        }

        public ushort ReadUShort()
        {
            ushort v = Binary.ReadUShort(this.buffer, this.Offset);
            this.Offset += 2;
            return v;
        }

        public void WriteUShort(ushort value)
        {
            this.Offset += 2;
            Binary.WriteUShort(this.buffer, value);
        }

        public ushort ReadLShort()
        {
            ushort v = Binary.ReadLShort(this.buffer, this.Offset);
            this.Offset += 2;
            return v;
        }

        public void WriteLShort(ushort value)
        {
            this.Offset += 2;
            Binary.WriteLShort(this.buffer, value);
        }

        public int ReadTriad()
        {
            int v = Binary.ReadTriad(this.buffer, this.Offset);
            this.Offset += 3;
            return v;
        }

        public void WriteTriad(int value)
        {
            this.Offset += 3;
            Binary.WriteTriad(this.buffer, value);
        }

        public int ReadLTriad()
        {
            int v = Binary.ReadLTriad(this.buffer, this.Offset);
            this.Offset += 3;
            return v;
        }

        public void WriteLTriad(int value)
        {
            this.Offset += 3;
            Binary.WriteLTriad(this.buffer, value);
        }

        public int ReadInt()
        {
            int v = Binary.ReadInt(this.buffer, this.Offset);
            this.Offset += 4;
            return v;
        }

        public void WriteInt(int value)
        {
            this.Offset += 4;
            Binary.WriteInt(this.buffer, value);
        }

        public uint ReadUInt()
        {
            uint v = Binary.ReadUInt(this.buffer, this.Offset);
            this.Offset += 4;
            return v;
        }

        public void WriteUInt(uint value)
        {
            this.Offset += 4;
            Binary.WriteUInt(this.buffer, value);
        }

        public uint ReadLInt()
        {
            uint v = Binary.ReadLInt(this.buffer, this.Offset);
            this.Offset += 4;
            return v;
        }

        public void WriteLInt(uint value)
        {
            this.Offset += 4;
            Binary.WriteLInt(this.buffer, value);
        }

        public long ReadLong()
        {
            long v = Binary.ReadLong(this.buffer, this.Offset);
            this.Offset += 8;
            return v;
        }

        public void WriteLong(long value)
        {
            this.Offset += 8;
            Binary.WriteLong(this.buffer, value);
        }

        public ulong ReadULong()
        {
            ulong v = Binary.ReadULong(this.buffer, this.Offset);
            this.Offset += 8;
            return v;
        }

        public void WriteULong(ulong value)
        {
            this.Offset += 8;
            Binary.WriteULong(this.buffer, value);
        }

        public ulong ReadLLong()
        {
            ulong v = Binary.ReadLLong(this.buffer, this.Offset);
            this.Offset += 8;
            return v;
        }

        public void WriteLLong(ulong value)
        {
            this.Offset += 8;
            Binary.WriteLLong(this.buffer, value);
        }

        public int ReadVarInt()
        {
            int offset = this.Offset;
            int v = Binary.ReadVarInt(this.buffer, ref offset);
            this.Offset = offset;
            return v;
        }

        public void WriteVarInt(int value)
        {
            int offset = this.Offset;
            Binary.WriteVarInt(this.buffer, value, out offset);
            this.Offset = offset + this.Offset;
        }

        public uint ReadUVarInt()
        {
            int offset = this.Offset;
            uint v = Binary.ReadUVarInt(this.buffer, ref offset);
            this.Offset = offset;
            return v;
        }

        public void WriteUVarInt(uint value)
        {
            int offset = this.Offset;
            Binary.WriteUVarInt(this.buffer, value, out offset);
            this.Offset = offset + this.Offset;
        }

        public int ReadSVarInt()
        {
            int offset = this.Offset;
            int v = Binary.ReadSVarInt(this.buffer, ref offset);
            this.Offset = offset;
            return v;
        }

        public void WriteSVarInt(int value)
        {
            int offset = this.Offset;
            Binary.WriteSVarInt(this.buffer, value, out offset);
            this.Offset = offset + this.Offset;
        }

        public long ReadVarLong()
        {
            int offset = this.Offset;
            long v = Binary.ReadVarLong(this.buffer, ref offset);
            this.Offset = offset;
            return v;
        }

        public void WriteVarLong(int value)
        {
            int offset = this.Offset;
            Binary.WriteVarLong(this.buffer, value, out offset);
            this.Offset = offset + this.Offset;
        }

        public ulong ReadUVarLong()
        {
            int offset = this.Offset;
            ulong v = Binary.ReadUVarLong(this.buffer, ref offset);
            this.Offset = offset;
            return v;
        }

        public void WriteUVarLong(ulong value)
        {
            int offset = this.Offset;
            Binary.WriteUVarLong(this.buffer, value, out offset);
            this.Offset = offset + this.Offset;
        }

        public long ReadSVarLong()
        {
            int offset = this.Offset;
            long v = Binary.ReadSVarLong(this.buffer, ref offset);
            this.Offset = offset;
            return v;
        }

        public void WriteSVarLong(long value)
        {
            int offset = this.Offset;
            Binary.WriteSVarLong(this.buffer, value, out offset);
            this.Offset = offset + this.Offset;
        }

        public float ReadFloat()
        {
            float v = Binary.ReadFloat(this.buffer, this.Offset);
            this.Offset += 4;
            return v;
        }

        public void WriteFloat(float value)
        {
            this.Offset += 4;
            Binary.WriteFloat(this.buffer, value);
        }

        public float ReadLFloat()
        {
            float v = Binary.ReadLFloat(this.buffer, this.Offset);
            this.Offset += 4;
            return v;
        }

        public void WriteLFloat(float value)
        {
            this.Offset += 4;
            Binary.WriteLFloat(this.buffer, value);
        }

        public double ReadDouble()
        {
            double v = Binary.ReadDouble(this.buffer, this.Offset);
            this.Offset += 8;
            return v;
        }

        public void WriteDouble(double value)
        {
            this.Offset += 8;
            Binary.WriteDouble(this.buffer, value);
        }

        public double ReadLDouble()
        {
            double v = Binary.ReadLDouble(this.buffer, this.Offset);
            this.Offset += 8;
            return v;
        }

        public void WriteLDouble(double value)
        {
            this.Offset += 8;
            Binary.WriteLDouble(this.buffer, value);
        }

        public string ReadFixedString()
        {
            string v = Binary.ReadFixedString(this.buffer, this.Offset);
            this.Offset += v.Length + 2;
            return v;
        }

        public void WriteFixedString(string value)
        {
            this.Offset += value.Length + 2;
            Binary.WriteFixedString(this.buffer, value);
        }

        public string ReadString()
        {
            int offset = this.Offset;
            string v = Binary.ReadString(this.buffer, ref offset);
            this.Offset += v.Length + (offset - this.Offset);
            return v;
        }

        public void WriteString(string value)
        {
            int offset = this.Offset;
            Binary.WriteString(this.buffer, value, out offset);
            this.Offset += value.Length + offset;
        }

        public IPEndPoint ReadIPEndPoint()
        {
            byte version = this.ReadByte();
            if (version == 4)
            {
                IPAddress ip = new IPAddress(new byte[]
                {
                    (byte) (~ReadByte() & 0xff),
                    (byte) (~ReadByte() & 0xff),
                    (byte) (~ReadByte() & 0xff),
                    (byte) (~ReadByte() & 0xff)
                });
                int port = this.ReadUShort();
                return new IPEndPoint(ip, port);
            }
            else if (version == 6)
            {
                this.ReadLShort();
                int port = ReadUShort();
                this.ReadLong();
                IPAddress ip = new IPAddress(this.ReadBytes(16));
                return new IPEndPoint(ip, port);
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        public void WriteIPEndPoint(IPEndPoint point)
        {
            this.WriteByte(4);
            this.WriteBytes(point.Address.GetAddressBytes());
            this.WriteUShort((ushort) point.Port);
        }

        public Guid ReadGuid()
        {
            return new Guid(this.ReadBytes(16));
        }

        public void WriteGuid(Guid guid)
        {
            this.WriteBytes(guid.ToByteArray());
        }

        public byte[] ReadBytes()
        {
            byte[] v = Binary.ReadBytes(this.buffer, this.Offset, this.Length - this.Offset);
            this.Offset = this.Length;
            return v;
        }

        public byte[] ReadBytes(int length)
        {
            byte[] v = Binary.ReadBytes(this.buffer, this.Offset, length);
            this.Offset += length;
            return v;
        }

        public byte[] ReadBytes(int offset, int length)
        {
            byte[] v = Binary.ReadBytes(this.buffer, offset, length);
            this.Offset = offset + length;
            return v;
        }

        public void WriteBytes(byte[] value)
        {
            this.Offset += value.Length;
            Binary.WriteBytes(this.buffer, value);
        }

        public void SetBuffer(byte[] buffer)
        {
            this.buffer = new List<byte>();
            this.Reset();
            this.WriteBytes(buffer);
            this.Reset();
        }

        public void Reset()
        {
            this.Offset = 0;
        }

        public byte[] ToArray()
        {
            return this.buffer.ToArray();
        }

        public virtual void Dispose()
        {
            this.buffer = null;
        }

        #endregion
    }
}
