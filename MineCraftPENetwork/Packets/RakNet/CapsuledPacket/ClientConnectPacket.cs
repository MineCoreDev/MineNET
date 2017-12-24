using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MineCraftPENetwork.Packets.RakNet.CapsuledPacket
{
    class ClientConnectPacket : RakNetPacket
    {
        public const byte ID = 0x09;

        public long clientID;
        public long session;
        public byte unknown;

        public override void Decode(BinaryReader reader)
        {
            BinaryReader br = reader;

            clientID = reader.ReadInt64();
            session = reader.ReadInt64();
            unknown = reader.ReadByte();
        }

        public override byte[] Encode()
        {
            throw new NotImplementedException();
        }
    }
}
