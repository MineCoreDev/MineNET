using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using MineCraftPENetwork.Packets.RakNet.PacketCapsule;

namespace MineCraftPENetwork.Packets.RakNet
{
    public abstract class DataPacket : RakNetPacket
    {
        public const byte ID = 0x00;

        const int UNRELIABLE = 0;
        const int UNRELIABLE_SEQUENCED = 1;
        const int RELIABLE = 2;
        const int RELIABLE_ORDERED = 3;
        const int RELIABLE_SEQUENCED = 4;
        const int UNRELIABLE_WITH_ACK_RECEIPT = 5;
        const int RELIABLE_WITH_ACK_RECEIPT = 6;
        const int RELIABLE_ORDERED_WITH_ACK_RECEIPT = 7;
        
        public PacketCapsuleBase[] packets;

        public abstract byte GetID();

        public override void Decode(BinaryReader reader)
        {
            BinaryReader br = reader;

            ReadPackets(br.ReadBytes((int)(br.BaseStream.Length - br.BaseStream.Position)));
        }

        public override byte[] Encode()
        {
            MemoryStream ms = new MemoryStream();
            BinaryWriter bw = new BinaryWriter(ms);

            bw.Write(GetID());
            WritePackets(bw);

            return ms.ToArray();
        }

        public void ReadPackets(byte[] payloadBuffer, bool readInternal = false)
        {
            MemoryStream ms = new MemoryStream(payloadBuffer);
            BinaryReader br = new BinaryReader(ms);

            List<PacketCapsuleBase> list = new List<PacketCapsuleBase>();

            var count = BitConverter.ToInt16(br.ReadBytes(3), 0);
            var offset = 0;
            while (br.BaseStream.Length - offset >= 0)
            {
                var capsuleID = br.ReadByte();
                switch (capsuleID)
                {
                    case PacketCapsule_0.CapsuleID:

                        var c0 = new PacketCapsule_0();
                        c0.Decode(br);

                        offset += c0.length - 1;
                        list.Add(c0);

                        break;

                    case PacketCapsule_1.CapsuleID:

                        var c1 = new PacketCapsule_1();
                        c1.Decode(br);

                        offset += c1.length - 1;
                        list.Add(c1);

                        break;

                    case PacketCapsule_2.CapsuleID:

                        var c2 = new PacketCapsule_2();
                        c2.Decode(br);

                        offset += c2.length - 1;
                        list.Add(c2);

                        break;

                    default:

                        var cd = new PacketCapsule_0();
                        cd.Decode(br);

                        offset += cd.length - 1;
                        list.Add(cd);

                        break;
                }
            }

            packets = list.ToArray();
        }

        public void WritePackets(BinaryWriter writer)
        {
            writer.Write(new byte[3]);
            foreach (var packet in packets)
            {
                if (packet is PacketCapsule_0)
                {
                    var tc = (PacketCapsule_0)packet;
                    writer.Write(tc.GetID());
                    writer.Write(tc.length);
                    writer.Write(tc.packet);
                }
                else if (packet is PacketCapsule_1)
                {
                    var tc = (PacketCapsule_1)packet;
                    writer.Write(tc.GetID());
                    writer.Write(tc.length);
                    writer.Write(tc.count);
                    writer.Write(tc.packet);
                }
                else if (packet is PacketCapsule_2)
                {
                    var tc = (PacketCapsule_2)packet;
                    writer.Write(tc.GetID());
                    writer.Write(tc.length);
                    writer.Write(tc.count);
                    writer.Write(tc.unknown);
                    writer.Write(tc.packet);
                }
            }
        }

        public byte[] GetBuffer(byte[] buffer, int start, int length)
        {
            List<byte> retBuffer = new List<byte>();
            for (int i = start; i < start + length; i++)
            {
                retBuffer.Add(buffer[i]);
            }

            return retBuffer.ToArray();
        }
    }
}
