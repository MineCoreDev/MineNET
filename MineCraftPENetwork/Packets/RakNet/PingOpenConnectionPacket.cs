using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MineCraftPENetwork.Packets.RakNet
{
    public class PingOpenConnectionPacket : RakNetPacket
    {
        public const byte ID = 0x1c;

        public long pingID;
        public long serverID;
        public byte[] magic;
        public string serverName;

        public override void Decode(BinaryReader reader)
        {
            throw new NotImplementedException();
        }

        public override byte[] Encode()
        {
            MemoryStream ms = new MemoryStream();
            BinaryWriter bw = new BinaryWriter(ms);

            bw.Write(ID);
            bw.Write(pingID);
            bw.Write(serverID);
            bw.Write(magic);
            bw.Write(serverName);

            return ms.ToArray();
        }
    }
}
