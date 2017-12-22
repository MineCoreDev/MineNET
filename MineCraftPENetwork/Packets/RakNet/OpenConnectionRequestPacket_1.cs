using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MineCraftPENetwork.Packets.RakNet
{
    public class OpenConnectionRequestPacket_1 : RakNetPacket
    {
        public const byte ID = 0x05;
        
        public byte[] magic;
        public byte rakNetVersion;
        public short mtuSize;

        public override void Decode(BinaryReader reader)
        {
            BinaryReader br = reader;
            
            magic = ReadMagic(br);
            rakNetVersion = br.ReadByte();
            mtuSize = (short)(br.BaseStream.Length - br.BaseStream.Position);
            mtuSize += 18;
        }

        public override byte[] Encode()
        {
            throw new NotImplementedException();
        }
    }
}
