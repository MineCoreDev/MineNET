using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MineCraftPENetwork.Packets.RakNet
{
    public class DataPacket_4 : RakNetPacket
    {
        public const byte ID = 0x84;

        public byte[] count;
        public byte[] payload;

        public override void Decode(BinaryReader reader)
        {
            BinaryReader br = reader;

            count = br.ReadBytes(3);
            payload = br.ReadBytes((int)(br.BaseStream.Length - br.BaseStream.Position));
        }

        public override byte[] Encode()
        {
            MemoryStream ms = new MemoryStream();
            BinaryWriter bw = new BinaryWriter(ms);

            bw.Write(count);
            bw.Write(payload);

            return ms.ToArray();
        }
    }
}
