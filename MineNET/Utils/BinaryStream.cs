using System;
using System.Collections.Generic;
using System.IO;

using MineNET.Values;

namespace MineNET.Utils
{
    public class BinaryStream : MemoryStream
    {
        public BinaryStream()
        {

        }

        public BinaryStream(byte[] buffer)
        {
            this.Write(buffer, 0, buffer.Length);
        }

        public bool ReadBoolean()
        {
            return Binary.ReadBoolean(this);
        }

        public void PutBoolean(bool v)
        {
            Binary.PutBoolean(this, v);
        }

        public new byte ReadByte()
        {
            return Binary.ReadByte(this);
        }

        public void PutByte(byte v)
        {
            Binary.PutByte(this, v);
        }

        public sbyte ReadSByte()
        {
            return Binary.ReadSByte(this);
        }

        public void PutSByte(sbyte v)
        {
            Binary.PutSByte(this, v);
        }

        public short ReadShort()
        {
            return Binary.ReadShort(this);
        }

        public void PutShort(short v)
        {
            Binary.PutShort(this, v);
        }

        public ushort ReadUShort()
        {
            return Binary.ReadUShort(this);
        }

        public void PutUShort(ushort v)
        {
            Binary.PutUShort(this, v);
        }

        public ushort ReadLShort()
        {
            return Binary.ReadLShort(this);
        }

        public void PutLShort(ushort v)
        {
            Binary.PutLShort(this, v);
        }

        public Int24 ReadLTriad()
        {
            return Binary.ReadLTriad(this);
        }

        public void PutLTriad(Int24 v)
        {
            Binary.PutLTriad(this, v);
        }

        public Int24 ReadTriad()
        {
            return Binary.ReadTriad(this);
        }

        public void PutTriad(Int24 v)
        {
            Binary.PutTriad(this, v);
        }

        public int ReadInt()
        {
            return Binary.ReadInt(this);
        }

        public void PutInt(int v)
        {
            Binary.PutInt(this, v);
        }

        public uint ReadUInt()
        {
            return Binary.ReadUInt(this);
        }

        public void PutUInt(uint v)
        {
            Binary.PutUInt(this, v);
        }

        public uint ReadLInt()
        {
            return Binary.ReadLInt(this);
        }

        public void PutLInt(uint v)
        {
            Binary.PutLInt(this, v);
        }

        public long ReadLong()
        {
            return Binary.ReadLong(this);
        }

        public void PutLong(long v)
        {
            Binary.PutLong(this, v);
        }

        public ulong ReadULong()
        {
            return Binary.ReadULong(this);
        }

        public void PutULong(ulong v)
        {
            Binary.PutULong(this, v);
        }

        public int ReadVarInt()
        {
            return Binary.ReadVarInt(this);
        }

        public void PutVarInt(int v)
        {
            Binary.PutVarInt(this, v);
        }

        public uint ReadVarUInt()
        {
            return Binary.ReadVarUInt(this);
        }

        public void PutVarUInt(uint v)
        {
            Binary.PutVarUInt(this, v);
        }

        public int ReadVarSInt()
        {
            return Binary.ReadVarSInt(this);
        }

        public void PutVarSInt(int v)
        {
            Binary.PutVarSInt(this, v);
        }

        public long ReadVarLong()
        {
            return Binary.ReadVarLong(this);
        }

        public void PutVarLong(long v)
        {
            Binary.PutVarLong(this, v);
        }

        public ulong ReadVarULong()
        {
            return Binary.ReadVarULong(this);
        }

        public void PutVarULong(ulong v)
        {
            Binary.PutVarULong(this, v);
        }

        public long ReadVarSLong()
        {
            return Binary.ReadVarSLong(this);
        }

        public void PutVarSLong(long v)
        {
            Binary.PutVarSLong(this, v);
        }

        public float ReadFloat()
        {
            return Binary.ReadFloat(this);
        }

        public void PutFloat(float v)
        {
            Binary.PutFloat(this, v);
        }

        public double ReadDouble()
        {
            return Binary.ReadDouble(this);
        }

        public void PutDouble(double v)
        {
            Binary.PutDouble(this, v);
        }

        public byte[] ReadPacketBuffer()
        {
            var v = (int)this.ReadVarUInt();
            return this.ReadBytes((int)this.Position, v);
        }

        public void PutPacketBuffer(byte[] buffer)
        {
            this.PutVarUInt((uint)buffer.Length);
            this.PutBytes(buffer);
        }

        public byte[] ReadBytes(int start)
        {
            return Binary.GetBytes(this, start);
        }

        public byte[] ReadBytes(int start, int len)
        {
            return Binary.GetBytes(this, start, len);
        }

        public void PutBytes(byte[] buffer)
        {
            for (int i = 0; i < buffer.Length; ++i)
            {
                this.WriteByte(buffer[i]);
            }
        }

        public byte[][] ReadSplitBytes(int len)
        {
            return Binary.SplitBytes(this, len);
        }

        public bool ReadOfEnd()
        {
            return this.Position < 0 || this.Position >= this.Length;
        }
    }
}
