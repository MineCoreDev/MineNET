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
        public const byte NETWORK_ID = 0xfe;

        public byte[] payload;

        protected CompressionLevel compressionLevel = CompressionLevel.Fastest;

        public override byte ID
        {
            get
            {
                return NETWORK_ID;
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

        public async Task<List<Packet>> GetPackets()
        {
            var list = new List<Packet>();
            var pb = new BinaryStream(this.payload);
            pb.Position = 0;

            while (!pb.ReadOfEnd())
            {
                byte[] b = await this.GetPacket(pb);
                Server.GetLogger().Log("§a[PEPacketHandle]ID: {0}", b[0]);
                var pk = PacketPool.GetPacketByID(b[0]);
                var bs = new BinaryStream(b);
                pk.PutBytes(bs.ReadBytes(1));
                bs.Close();
                list.Add(pk);
            }

            return list;
        }

        public Task<byte[]> GetPacket(BinaryStream buffer)
        {
            var task = new Task<byte[]>(() =>
            {
                return buffer.ReadPacketBuffer();
            });
            task.Start();
            return task;
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
