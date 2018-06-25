using MineNET.Utils;
using System;
using System.IO;
using System.IO.Compression;

namespace MineNET.Network.RakNetPackets
{
    public class BatchPacket : RakNetPacket
    {
        public override byte MessageID { get; } = RakNetProtocol.BatchPacket;

        public byte[] Payload { get; set; } = new byte[0];
        public CompressionLevel CompressionLevel { get; set; } = CompressionLevel.Fastest;

        public override void Encode()
        {
            base.Encode();

            this.Encode_ZLIB();
            this.WriteBytes(this.Payload);
        }

        public override void Decode()
        {
            base.Decode();

            this.Payload = this.ReadBytes();
            this.Decode_ZLIB();
        }

        private void Encode_ZLIB()
        {
            MemoryStream bs = new MemoryStream();
            bs.WriteByte(0x78);

            WriteCompressionLevel(bs);

            int sum = 0;
            using (ZlibStream ds = new ZlibStream(bs, this.CompressionLevel, true))
            {
                ds.Write(this.Payload, 0, this.Payload.Length);
                sum = ds.Checksum;
            }

            byte[] checksumBytes = BitConverter.GetBytes(sum);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(checksumBytes);
            }
            bs.Write(checksumBytes, 0, checksumBytes.Length);

            this.Payload = bs.GetBuffer();
            bs.Close();
        }

        private void Decode_ZLIB()
        {
            MemoryStream bs = new MemoryStream(this.Payload);
            if (bs.ReadByte() != 0x78)
            {
                throw new InvalidDataException("Incorrect ZLib header. Expected 0x78 0x9C");
            }
            bs.ReadByte();
            using (ZlibStream ds = new ZlibStream(bs, CompressionMode.Decompress, false))
            {
                MemoryStream c = new MemoryStream();
                ds.CopyTo(c);
                this.Payload = c.ToArray();
                c.Close();
            }
            bs.Close();
        }

        void WriteCompressionLevel(MemoryStream stream)
        {
            if (this.CompressionLevel == CompressionLevel.NoCompression)
            {
                stream.WriteByte(0x01);
            }
            else if (this.CompressionLevel == CompressionLevel.Fastest)
            {
                stream.WriteByte(0x9C);
            }
            else if (this.CompressionLevel == CompressionLevel.Optimal)
            {
                stream.WriteByte(0xda);
            }
        }
    }
}
