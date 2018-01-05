using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

using MineNET.Utils;

namespace MineNET.Network.Packets
{
    public class BatchPacket : Packet
    {
        public byte[] payload;

        protected CompressionLevel compressionLevel = CompressionLevel.Fastest;

        public override byte ID
        {
            get
            {
                return 0xfe;
            }
        }

        public override void Encode()
        {
            this.PutByte(ID);
            this.PutBytes(this.Zlib_Encode(payload));
        }

        public override void Decode()
        {
            this.Reset();
            byte id = this.ReadByte();
            if (id != ID)
            {
                return;
            }
            this.payload = this.Zlib_Decode(this.ReadBytes(1));
        }

        public async void GetPackets()
        {
            this.Position = 1;
            while (!this.ReadOfEnd())
            {
                var r = await this.GetPacket();
                Console.WriteLine("[MCPE?]" + r[0]);
            }
        }

        public Task<byte[]> GetPacket()
        {
            return new Task<byte[]>(() =>
            {
                return this.ReadPacketBuffer();
            });
        }

        public byte[] Zlib_Encode(byte[] buffer)
        {
            BinaryStream bs = new BinaryStream(buffer);
            DeflateStream ds = new DeflateStream(bs, compressionLevel);
            ds.Close();
            var result = bs.ToArray();
            bs.Close();

            return result;
        }

        public byte[] Zlib_Decode(byte[] buffer)
        {
            MemoryStream bs = new MemoryStream(buffer);
            if (bs.ReadByte() != 0x78)
            {
                throw new InvalidDataException("Incorrect ZLib header. Expected 0x78 0x9C");
            }
            bs.ReadByte();
            using (DeflateStream ds = new DeflateStream(bs, CompressionMode.Decompress, false))
            {
                var c = new MemoryStream();
                ds.CopyTo(c);

                Console.WriteLine(buffer.Length + "->" + c.ToArray().Length);

                return c.ToArray();
            }
        }
    }
}
