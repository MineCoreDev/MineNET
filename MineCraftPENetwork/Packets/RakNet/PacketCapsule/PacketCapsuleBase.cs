using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MineCraftPENetwork.Packets.RakNet.PacketCapsule
{
    public abstract class PacketCapsuleBase : RakNetPacket
    {
        public ushort length;
        public byte[] packet;

        public abstract byte GetID();

        public override void Decode(BinaryReader reader)
        {
            BinaryReader br = reader;

            length = br.ReadUInt16();
            packet = br.ReadBytes(length);
        }

        public override byte[] Encode()
        {
            MemoryStream ms = new MemoryStream();
            BinaryWriter bw = new BinaryWriter(ms);

            bw.Write(GetID());
            bw.Write(length);
            bw.Write(packet);

            return ms.ToArray();
        }

        public void SetLength(int packetLength)
        {
            length = (ushort)(packetLength / 8);
        }
    }
}
