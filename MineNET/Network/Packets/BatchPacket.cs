using System;
using System.IO;
using System.IO.Compression;
using MineNET.Utils;

namespace MineNET.Network.Packets
{
    public class BatchPacket : DataPacket
    {
        public const int ID = 0xfe;

        public override byte PacketID
        {
            get
            {
                return ID;
            }
        }

        byte[] payload;
        public byte[] Payload
        {
            get
            {
                return payload;
            }

            set
            {
                payload = value;
            }
        }

        public override void Encode()
        {
            WriteByte(PacketID);
            Encode_ZLIB();
            WriteBytes(payload);
        }

        public override void Decode()
        {
            ReadByte();
            payload = ReadBytes();
            Decode_ZLIB();
        }

        void Encode_ZLIB()
        {
            MemoryStream bs = new MemoryStream();
            bs.WriteByte(0x78);
            bs.WriteByte(0x9c);//TODO: CompressionLevel
            var sum = 0;
            using (ZlibStream ds = new ZlibStream(bs, CompressionLevel.Fastest, true))
            {
                ds.Write(payload, 0, payload.Length);
                sum = ds.Checksum;
            }

            var checksumBytes = BitConverter.GetBytes(sum);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(checksumBytes);
            }
            bs.Write(checksumBytes, 0, checksumBytes.Length);

            payload = bs.GetBuffer();
            bs.Close();
        }

        void Decode_ZLIB()
        {
            MemoryStream bs = new MemoryStream(payload);
            if (bs.ReadByte() != 0x78)
            {
                throw new InvalidDataException("Incorrect ZLib header. Expected 0x78 0x9C");
            }
            bs.ReadByte();
            using (ZlibStream ds = new ZlibStream(bs, CompressionMode.Decompress, false))
            {
                var c = new MemoryStream();
                ds.CopyTo(c);
                payload = c.ToArray();
                Logger.Log("Len: {0}", payload.Length);
                c.Close();
            }
            bs.Close();
        }
    }
}
