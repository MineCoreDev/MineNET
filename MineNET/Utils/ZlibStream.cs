﻿using System.IO;
using System.IO.Compression;

namespace MineNET.Utils
{
    public class ZlibStream : DeflateStream
    {
        private uint adler32 = 1;

        private const int mod = 65521;//mod

        public int Checksum => (int) this.adler32;

        private uint Update(uint adler, byte[] s, int offset, int count)
        {
            uint l = (ushort) adler;
            ulong h = adler >> 16;
            int p = 0;
            for (; p < (count & 7); ++p)
            {
                l += s[offset + p];
                h += l;
            }

            for (; p < count; p += 8)
            {
                int idx = offset + p;
                l += s[idx];
                h += l;
                l += s[idx + 1];
                h += l;
                l += s[idx + 2];
                h += l;
                l += s[idx + 3];
                h += l;
                l += s[idx + 4];
                h += l;
                l += s[idx + 5];
                h += l;
                l += s[idx + 6];
                h += l;
                l += s[idx + 7];
                h += l;
            }

            return (uint) (((h % mod) << 16) | (l % mod));
        }

        public ZlibStream(Stream stream, CompressionLevel level, bool leaveOpen) : base(stream, level, leaveOpen)
        {
        }

        public ZlibStream(Stream stream, CompressionMode mode, bool leaveOpen) : base(stream, mode, leaveOpen)
        {
        }

        public override void Write(byte[] array, int offset, int count)
        {
            this.adler32 = Update(this.adler32, array, offset, count);
            base.Write(array, offset, count);
        }
    }
}
