using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MineCraftPENetwork.Packets.RakNet.PacketCapsule
{
    public class PacketCapsule_2 : PacketCapsuleBase
    {
        public const byte CapsuleID = 0x60;
        
        public byte[] count;
        public byte[] unknown;

        public override byte GetID()
        {
            return CapsuleID;
        }

        public override void Decode(BinaryReader reader)
        {
            BinaryReader br = reader;

            length = br.ReadUInt16();
            count = br.ReadBytes(3);
            unknown = br.ReadBytes(4);
            packet = br.ReadBytes(length);
        }

        public override byte[] Encode()
        {
            MemoryStream ms = new MemoryStream();
            BinaryWriter bw = new BinaryWriter(ms);

            bw.Write(GetID());
            bw.Write(length);
            bw.Write(count);
            bw.Write(unknown);
            bw.Write(packet);

            return ms.ToArray();
        }
    }
}
