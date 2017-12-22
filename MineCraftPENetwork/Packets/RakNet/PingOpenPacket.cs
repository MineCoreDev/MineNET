using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MineCraftPENetwork.Packets.RakNet
{
    public class PingOpenPacket : RakNetPacket
    {
        public const byte ID = 0x01;

        public long pingID;
        public byte[] magic;

        public override void Decode(BinaryReader reader)
        {
            BinaryReader br = reader;

            pingID = br.ReadInt64();
            magic = ReadMagic(br);
        }

        public override byte[] Encode()
        {
            MemoryStream ms = new MemoryStream();
            BinaryWriter bw = new BinaryWriter(ms);

            bw.Write(ID);
            bw.Write(pingID);
            bw.Write(magic);

            return ms.ToArray();
        }
    }
}
