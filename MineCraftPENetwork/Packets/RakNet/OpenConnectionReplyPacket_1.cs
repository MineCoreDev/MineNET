using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MineCraftPENetwork.Packets.RakNet
{
    public class OpenConnectionReplyPacket_1 : RakNetPacket
    {
        public const byte ID = 0x06;
        
        public byte[] magic;
        public long serverID;
        public byte serverSecurity;
        public short mtuSize;

        public override void Decode(BinaryReader reader)
        {
            BinaryReader br = reader;
            
            magic = ReadMagic(br);
            serverID = br.ReadInt64();
            serverSecurity = br.ReadByte();
            mtuSize = br.ReadInt16();
        }

        public override byte[] Encode()
        {
            MemoryStream ms = new MemoryStream();
            BinaryWriter bw = new BinaryWriter(ms);

            bw.Write(ID);
            WriteMagic(bw, magic);
            bw.Write(serverID);
            bw.Write(serverSecurity);
            bw.Write(mtuSize);

            return ms.ToArray();
        }
    }
}
