using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MineCraftPENetwork.Packets.RakNet
{
    class ACKPacket : RakNetPacket
    {
        public const byte ID = 0xC0;

        public short count;
        public bool minEqualsMax;
        public byte[] minPacketNum;
        public byte[] maxPacketNum;

        public override void Decode(BinaryReader reader)
        {
            BinaryReader br = reader;

            count = br.ReadInt16();
            minEqualsMax = br.ReadBoolean();
            minPacketNum = br.ReadBytes(3);
            if (!minEqualsMax)
            {
                maxPacketNum = br.ReadBytes(3);
            }
        }

        public override byte[] Encode()
        {
            MemoryStream ms = new MemoryStream();
            BinaryWriter bw = new BinaryWriter(ms);

            bw.Write(ID);
            bw.Write(count);
            bw.Write(minEqualsMax);
            bw.Write(minPacketNum);
            if (!minEqualsMax)
            {
                bw.Write(maxPacketNum);
            }

            return ms.ToArray();
        }
    }
}
