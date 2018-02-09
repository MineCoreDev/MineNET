using System;
using System.IO;
using System.Net;

namespace MineNET.Utils
{
    public class BinaryStream : MemoryStream
    {
        public BinaryStream()
        {

        }

        public BinaryStream(byte[] buffer)
        {
            Binary.WriteBytes(this, buffer);
            this.Reset();
        }

        public bool ReadBool()
        {
            return Binary.ReadBool(this);
        }

        public void WriteBool(bool value)
        {
            Binary.WriteBool(this, value);
        }

        public new byte ReadByte()
        {
            return Binary.ReadByte(this);
        }

        public void WirteByte(byte value)
        {
            Binary.WriteByte(this, value);
        }

        public sbyte ReadSByte()
        {
            return Binary.ReadSByte(this);
        }

        public void WriteSByte(sbyte value)
        {
            Binary.WriteSByte(this, value);
        }

        public short ReadShort()
        {
            return Binary.ReadShort(this);
        }

        public void WriteShort(short value)
        {
            Binary.WriteShort(this, value);
        }

        public ushort ReadUShort()
        {
            return Binary.ReadUShort(this);
        }

        public void WriteUShort(ushort value)
        {
            Binary.WriteUShort(this, value);
        }

        public short ReadLShort()
        {
            return Binary.ReadLShort(this);
        }

        public void WriteLShort(short value)
        {
            Binary.WriteLShort(this, value);
        }

        public int ReadTriad()
        {
            return Binary.ReadTriad(this);
        }

        public void WriteTriad(int value)
        {
            Binary.WriteTriad(this, value);
        }

        public int ReadLTriad()
        {
            return Binary.ReadLTriad(this);
        }

        public void WriteLTriad(int value)
        {
            Binary.WriteLTriad(this, value);
        }

        public int ReadInt()
        {
            return Binary.ReadInt(this);
        }

        public void WriteInt(int value)
        {
            Binary.WriteInt(this, value);
        }

        public uint ReadUInt()
        {
            return Binary.ReadUInt(this);
        }

        public void WriteUIntt(uint value)
        {
            Binary.WriteUInt(this, value);
        }

        public int ReadLInt()
        {
            return Binary.ReadLInt(this);
        }

        public void WriteLInt(int value)
        {
            Binary.WriteLInt(this, value);
        }

        public long ReadLong()
        {
            return Binary.ReadLong(this);
        }

        public void WriteLong(long value)
        {
            Binary.WriteLong(this, value);
        }

        public ulong ReadULong()
        {
            return Binary.ReadULong(this);
        }

        public void WriteULong(ulong value)
        {
            Binary.WriteULong(this, value);
        }

        public long ReadLLong()
        {
            return Binary.ReadLLong(this);
        }

        public void WriteLLong(long value)
        {
            Binary.WriteLLong(this, value);
        }

        //TODO: VarInt...

        public float ReadFloat()
        {
            return Binary.ReadFloat(this);
        }

        public void WriteFloat(float value)
        {
            Binary.WriteFloat(this, value);
        }

        public double ReadDouble()
        {
            return Binary.ReadDouble(this);
        }

        public void WriteDouble(double value)
        {
            Binary.WriteDouble(this, value);
        }

        public string ReadFixedString()
        {
            return Binary.ReadFixedString(this);
        }

        public void WriteFixedString(string value)
        {
            Binary.WriteFixedString(this, value);
        }

        //TODO: String...

        public IPEndPoint ReadIPEndPoint()
        {
            byte version = ReadByte();
            if (version == 4)
            {
                IPAddress ip = new IPAddress(new byte[]
                {
                    (byte)(~ReadByte() & 0xff),
                    (byte)(~ReadByte() & 0xff),
                    (byte)(~ReadByte() & 0xff),
                    (byte)(~ReadByte() & 0xff)
                });
                int port = (ushort)ReadLShort();
                return new IPEndPoint(ip, port);
            }
            /*else if (version == 6)
            {
                //TODO: Support IPv6...
                return null;
            }*/
            else
            {
                throw new NotSupportedException($"IPv{version} Not Support!");
            }
        }

        public void WriteIPEndPoint(IPEndPoint endPoint)
        {
            byte version = 4;

            WriteByte(version);
            if (version == 4)
            {
                WriteBytes(endPoint.Address.GetAddressBytes());
                WriteLShort((short)endPoint.Port);
            }
            else
            {
                throw new NotSupportedException($"IPv{version} Not Support!");
            }
        }

        public byte[] ReadBytes(int start, int length)
        {
            return Binary.ReadBytes(this, start, length);
        }

        public byte[] ReadBytes(int length)
        {
            return Binary.ReadBytes(this, (int)this.Position, length);
        }

        public byte[] ReadBytes()
        {
            return Binary.ReadBytes(this, (int)this.Position, (int)this.Length - (int)this.Position);
        }

        public void WriteBytes(byte[] value)
        {
            Binary.WriteBytes(this, value);
        }

        public new byte[] GetBuffer()
        {
            return this.GetResult();
        }

        public void SetBuffer(byte[] buffer)
        {
            this.Reset();
            this.WriteBytes(buffer);
            this.Reset();
        }

        public byte[] GetResult()
        {
            return this.ToArray();
        }

        public void Reset()
        {
            Binary.Reset(this);
        }

        public bool EndOfStream(Stream stream)
        {
            return Binary.EndOfStream(this);
        }
    }
}
