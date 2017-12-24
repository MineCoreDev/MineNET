using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MineCraftPENetwork.Packets.RakNet
{
    public class OpenConnectionReplyPacket_2 : RakNetPacket
    {
        public const byte ID = 0x08;
        
        public byte[] magic;
        public long serverID;
        public short clientPort;
        public short mtuSize;
        public byte security;

        public override void Decode(BinaryReader reader)
        {
            throw new NotImplementedException();
        }

        public override byte[] Encode()
        {
            MemoryStream ms = new MemoryStream();
            BinaryWriter bw = new BinaryWriter(ms);

            bw.Write(ID);
            WriteMagic(bw, magic);
            bw.Write(serverID);
            bw.Write(clientPort);
            bw.Write(mtuSize);
            bw.Write(security);

            return ms.ToArray();
        }
    }
}
