using System;
using System.Net;

namespace MineNET.Utils
{
    public class BinaryStream : IDisposable, ICloneable<BinaryStream>
    {
        MemorySpan buffer = new MemorySpan(new byte[0]);

        public int Offset
        {
            get
            {
                return this.buffer.Offset;
            }

            set
            {
                this.buffer.Offset = value;
            }
        }

        public int Length
        {
            get
            {
                return this.buffer.Length;
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
        }

        public BinaryStream(byte[] buffer, bool reset = true)
        {
            this.buffer = new MemorySpan(buffer);
            if (reset)
            {
                this.Reset();
            }
        }

        #endregion

        #region Public Method

        public bool ReadBool()
        {
            return Binary.ReadBool(ref this.buffer);
        }

        public void WriteBool(bool value)
        {
            Binary.WriteBool(ref this.buffer, value);
        }

        public byte ReadByte()
        {
            return Binary.ReadByte(ref this.buffer);
        }

        public void WriteByte(byte value)
        {
            Binary.WriteByte(ref this.buffer, value);
        }

        public sbyte ReadSByte()
        {
            return Binary.ReadSByte(ref this.buffer);
        }

        public void WriteSByte(sbyte value)
        {
            Binary.WriteSByte(ref this.buffer, value);
        }

        public short ReadShort()
        {
            return Binary.ReadShort(ref this.buffer);
        }

        public void WriteShort(short value)
        {
            Binary.WriteShort(ref this.buffer, value);
        }

        public ushort ReadUShort()
        {
            return Binary.ReadUShort(ref this.buffer);
        }

        public void WriteUShort(ushort value)
        {
            Binary.WriteUShort(ref this.buffer, value);
        }

        public ushort ReadLShort()
        {
            return Binary.ReadLShort(ref this.buffer);
        }

        public void WriteLShort(ushort value)
        {
            Binary.WriteLShort(ref this.buffer, value);
        }

        public int ReadTriad()
        {
            return Binary.ReadTriad(ref this.buffer);
        }

        public void WriteTriad(int value)
        {
            Binary.WriteTriad(ref this.buffer, value);
        }

        public int ReadLTriad()
        {
            return Binary.ReadLTriad(ref this.buffer);
        }

        public void WriteLTriad(int value)
        {
            Binary.WriteLTriad(ref this.buffer, value);
        }

        public int ReadInt()
        {
            return Binary.ReadInt(ref this.buffer);
        }

        public void WriteInt(int value)
        {
            Binary.WriteInt(ref this.buffer, value);
        }

        public uint ReadUInt()
        {
            return Binary.ReadUInt(ref this.buffer);
        }

        public void WriteUInt(uint value)
        {
            Binary.WriteUInt(ref this.buffer, value);
        }

        public uint ReadLInt()
        {
            return Binary.ReadLInt(ref this.buffer);
        }

        public void WriteLInt(uint value)
        {
            Binary.WriteLInt(ref this.buffer, value);
        }

        public long ReadLong()
        {
            return Binary.ReadLong(ref this.buffer);
        }

        public void WriteLong(long value)
        {
            Binary.WriteLong(ref this.buffer, value);
        }

        public ulong ReadULong()
        {
            return Binary.ReadULong(ref this.buffer);
        }

        public void WriteULong(ulong value)
        {
            Binary.WriteULong(ref this.buffer, value);
        }

        public ulong ReadLLong()
        {
            return Binary.ReadLLong(ref this.buffer);
        }

        public void WriteLLong(ulong value)
        {
            Binary.WriteLLong(ref this.buffer, value);
        }

        public int ReadVarInt()
        {
            return Binary.ReadVarInt(ref this.buffer);
        }

        public void WriteVarInt(int value)
        {
            Binary.WriteVarInt(ref this.buffer, value);
        }

        public uint ReadUVarInt()
        {
            return Binary.ReadUVarInt(ref this.buffer);
        }

        public void WriteUVarInt(uint value)
        {
            Binary.WriteUVarInt(ref this.buffer, value);
        }

        public int ReadSVarInt()
        {
            return Binary.ReadSVarInt(ref this.buffer);
        }

        public void WriteSVarInt(int value)
        {
            Binary.WriteSVarInt(ref this.buffer, value);
        }

        public long ReadVarLong()
        {
            return Binary.ReadVarLong(ref this.buffer);
        }

        public void WriteVarLong(long value)
        {
            Binary.WriteVarLong(ref this.buffer, value);
        }

        public ulong ReadUVarLong()
        {
            return Binary.ReadUVarLong(ref this.buffer);
        }

        public void WriteUVarLong(ulong value)
        {
            Binary.WriteUVarLong(ref this.buffer, value);
        }

        public long ReadSVarLong()
        {
            return Binary.ReadSVarLong(ref this.buffer);
        }

        public void WriteSVarLong(long value)
        {
            Binary.WriteSVarLong(ref this.buffer, value);
        }

        public float ReadFloat()
        {
            return Binary.ReadFloat(ref this.buffer);
        }

        public void WriteFloat(float value)
        {
            Binary.WriteFloat(ref this.buffer, value);
        }

        public float ReadLFloat()
        {
            return Binary.ReadLFloat(ref this.buffer);
        }

        public void WriteLFloat(float value)
        {
            Binary.WriteLFloat(ref this.buffer, value);
        }

        public double ReadDouble()
        {
            return Binary.ReadDouble(ref this.buffer);
        }

        public void WriteDouble(double value)
        {
            Binary.WriteDouble(ref this.buffer, value);
        }

        public double ReadLDouble()
        {
            return Binary.ReadLDouble(ref this.buffer);
        }

        public void WriteLDouble(double value)
        {
            Binary.WriteLDouble(ref this.buffer, value);
        }

        public string ReadFixedString()
        {
            return Binary.ReadFixedString(ref this.buffer);
        }

        public void WriteFixedString(string value)
        {
            Binary.WriteFixedString(ref this.buffer, value);
        }

        public string ReadString()
        {
            return Binary.ReadString(ref this.buffer);
        }

        public void WriteString(string value)
        {
            Binary.WriteString(ref this.buffer, value);
        }

        public IPEndPoint ReadIPEndPoint()
        {
            byte version = this.ReadByte();
            if (version == 4)
            {
                IPAddress ip = new IPAddress(ReadBytes(4));
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
            return Binary.ReadBytes(ref this.buffer);
        }

        public byte[] ReadBytes(int length)
        {
            return Binary.ReadBytes(ref this.buffer, length);
        }

        public byte[] ReadBytes(int offset, int length)
        {
            return Binary.ReadBytes(ref this.buffer, offset, length);
        }

        public void WriteBytes(byte[] value)
        {
            Binary.WriteBytes(ref this.buffer, value);
        }

        public void SetBuffer(byte[] buffer)
        {
            this.buffer = new MemorySpan(buffer);
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

        public void Reservation(int len)
        {
            this.buffer.Reservation(len);
        }

        public virtual void Dispose()
        {
            this.buffer.Dispose();
        }

        public BinaryStream Clone()
        {
            return new BinaryStream(this.ToArray());
        }

        object ICloneable.Clone()
        {
            return new BinaryStream(this.ToArray());
        }

        #endregion
    }
}
