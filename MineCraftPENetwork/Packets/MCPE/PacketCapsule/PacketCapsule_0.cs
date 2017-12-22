using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MineCraftPENetwork.Packets.MCPE.PacketCapsule
{
    public class PacketCapsule_0 : Packet
    {
        public const byte CapsuleID = 0x00;

        public short length;
        public byte[] packet;

        public override void Decode(BinaryReader reader)
        {
            BinaryReader br = reader;

            length = br.ReadInt16();
            packet = br.ReadBytes((int)(br.BaseStream.Length - br.BaseStream.Position));
        }

        public override byte[] Encode()
        {
            MemoryStream ms = new MemoryStream();
            BinaryWriter bw = new BinaryWriter(ms);

            bw.Write(length);
            bw.Write(packet);

            return ms.ToArray();
        }

        public void SetLength(int packetLength)
        {
            length = (short)(packetLength / 8);
        }
    }
}
