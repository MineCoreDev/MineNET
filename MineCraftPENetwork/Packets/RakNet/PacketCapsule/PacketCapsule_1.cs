using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MineCraftPENetwork.Packets.RakNet.PacketCapsule
{
    public class PacketCapsule_1 : PacketCapsuleBase
    {
        public const byte CapsuleID = 0x40;
        
        public byte[] count;

        public override byte GetID()
        {
            return CapsuleID;
        }

        public override void Decode(BinaryReader reader)
        {
            BinaryReader br = reader;

            length = br.ReadUInt16();
            count = br.ReadBytes(3);
            packet = br.ReadBytes(length);
        }

        public override byte[] Encode()
        {
            MemoryStream ms = new MemoryStream();
            BinaryWriter bw = new BinaryWriter(ms);

            bw.Write(GetID());
            bw.Write(length);
            bw.Write(count);
            bw.Write(packet);

            return ms.ToArray();
        }
    }
}
