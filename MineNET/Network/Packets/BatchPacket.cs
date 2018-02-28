using MineNET.Utils;
using System;
using System.IO;
using System.IO.Compression;

namespace MineNET.Network.Packets
{
    public class BatchPacket : DataPacket
    {
        public const int ID = ProtocolInfo.BATCH_PACKET;

        public override byte PacketID
        {
            get
            {
                return BatchPacket.ID;
            }
        }

        public byte[] Payload { get; set; }

        public override void Encode()
        {
            this.WriteByte(this.PacketID);
            this.Encode_ZLIB();
            this.WriteBytes(this.Payload);
        }

        public override void Decode()
        {
            this.ReadByte();
            this.Payload = this.ReadBytes();
            this.Decode_ZLIB();
        }

        private void Encode_ZLIB()
        {
            MemoryStream bs = new MemoryStream();
            bs.WriteByte(0x78);
            bs.WriteByte(0x9c);//TODO: CompressionLevel
            int sum = 0;
            using (ZlibStream ds = new ZlibStream(bs, CompressionLevel.Fastest, true))
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
    }
}
