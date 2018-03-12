﻿using System;
using System.Collections.Generic;

namespace MineNET.Utils
{
    public struct MemorySpan : IDisposable
    {
        byte[] _buffer;
        int _offset;

        public MemorySpan(byte[] buffer, int offset = 0)
        {
            this._buffer = buffer;
            this._offset = offset;
        }

        public int Offset
        {
            get
            {
                return this._offset;
            }

            set
            {
                this._offset = value;
            }
        }

        public int Length
        {
            get
            {
                return this._buffer.Length;
            }
        }

        public byte this[int index]
        {
            get
            {
                return this._buffer[index];
            }

            set
            {
                this._buffer[index] = value;
            }
        }

        public byte[] ToArray()
        {
            return this._buffer;
        }

        public byte ReadByte()
        {
            return this._buffer[this._offset++];
        }

        public void WriteByte(byte value)
        {
            Array.Resize(ref this._buffer, this._offset + 1);
            Buffer.BlockCopy(new byte[] { value }, 0, this._buffer, this._offset++, 1);
        }

        public byte[] ReadBytes(int offset, int length)
        {
            List<byte> bytes = new List<byte>();
            this._offset = offset;
            for (int i = 0; i < length; ++i)
            {
                bytes.Add(this.ReadByte());
            }
            return bytes.ToArray();
        }

        public byte[] ReadBytes(int length)
        {
            return this.ReadBytes(this._offset, length);
        }

        public byte[] ReadBytes()
        {
            return this.ReadBytes(this._offset, this.Length - this.Offset);
        }

        public void WriteBytes(byte[] value)
        {
            Array.Resize(ref this._buffer, this.Length + value.Length);
            Buffer.BlockCopy(value, 0, this._buffer, this._offset, value.Length);
            this._offset += value.Length;
        }

        public void Resize(int length)
        {
            Array.Resize(ref this._buffer, length);
        }

        public void Dispose()
        {
            _buffer = null;
            _offset = 0;
        }
    }
}
