using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MineCraftPENetwork.Packets.RakNet
{
    public class OpenConnectionRequestPacket_2 : RakNetPacket
    {
        public const byte ID = 0x07;
        
        public byte[] magic;
        public byte security;
        public int cookie;
        public short clientPort;
        public short mtuSize;
        public long clientID;

        public override void Decode(BinaryReader reader)
        {
            BinaryReader br = reader;
            
            magic = ReadMagic(br);
            security = br.ReadByte();
            cookie = br.ReadInt32();
            clientPort = br.ReadInt16();
            mtuSize = br.ReadInt16();
            clientID = br.ReadInt64();
        }

        public override byte[] Encode()
        {
            throw new NotImplementedException();
        }
    }
}
