using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MineCraftPENetwork.Packets.MCPE.PacketCapsule
{
    public class PacketCapsule_1 : Packet
    {
        public const byte CapsuleID = 0x40;

        public short length;
        public byte[] count;
        public byte[] packet;

        public override void Decode(BinaryReader reader)
        {
            BinaryReader br = reader;

            length = br.ReadInt16();
            count = br.ReadBytes(3);
            packet = br.ReadBytes((int)(br.BaseStream.Length - br.BaseStream.Position));
        }

        public override byte[] Encode()
        {
            MemoryStream ms = new MemoryStream();
            BinaryWriter bw = new BinaryWriter(ms);

            bw.Write(length);
            bw.Write(count);
            bw.Write(packet);

            return ms.ToArray();
        }

        public void SetLength(int packetLength)
        {
            length = (short)(packetLength / 8);
        }
    }
}
