using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MineCraftPENetwork.Protocol
{
    public class EncapsulatedPacket
    {
        public int reliability;
        public bool hasSplit = false;
        public int length = 0;
        public int messageIndex = -1;
        public int orderIndex = -1;
        public int orderChannel = 0;
        public int splitCount = 0;
        public int splitID = 0;
        public int splitIndex = 0;
        public byte[] buffer;
        public bool needACK = false;
        public int identifierACK = -1;

        public static EncapsulatedPacket FromBinary(byte[] binary, ref int offset, bool isInternal = false)
        {

            var packet = new EncapsulatedPacket();

            var flags = binary[0] & 0xff;
            var reliability = ((flags & Convert.ToByte("011100000", 2)) >> 5);
            var hasSplit = ((flags & Convert.ToByte("00010000", 2)) > 0);
            var length = 0;

            packet.reliability = reliability;
            packet.hasSplit = hasSplit;
            if (isInternal)
            {
                length = Packet.BytesToInt(RakNet.GetBuffer(binary, 1, 4));
                packet.identifierACK = Packet.BytesToInt(RakNet.GetBuffer(binary, 5, 4));
                offset = 9;
            }
            else
            {
                //Console.WriteLine("[Len]" + Packet.SwapInt16(BitConverter.ToInt16(binary, 1)));
                Console.WriteLine("[Len]" + (int)Math.Ceiling((double)(Packet.SwapUInt16((ushort)BitConverter.ToInt16(binary, 1)) / 8)));
                length = (int)Math.Ceiling((double)(Packet.SwapUInt16((ushort)BitConverter.ToInt16(binary, 1)) / 8));
                offset = 3;
                packet.identifierACK = 0;
            }

            if (reliability > PacketReliability.UNRELIABLE)
            {
                if (reliability >= PacketReliability.RELIABLE && reliability != PacketReliability.UNRELIABLE_WITH_ACK_RECEIPT)
                {
                    packet.messageIndex = Packet.LTriadToInt32(RakNet.GetBuffer(binary, offset, 3));
                    offset += 3;
                }

                if (reliability <= PacketReliability.RELIABLE_SEQUENCED && reliability != PacketReliability.RELIABLE)
                {
                    packet.orderIndex = Packet.LTriadToInt32(RakNet.GetBuffer(binary, offset, 3));
                    offset += 3;
                    try
                    {
                        packet.orderChannel = binary[offset++];
                    }
                    catch (Exception e)
                    {
                        packet.orderChannel = 0;
                    }
                }
            }

            if (hasSplit)
            {
                packet.splitCount = Packet.BytesToInt(RakNet.GetBuffer(binary, offset, 4));
                offset += 4;
                packet.splitID = Packet.BytesToShort(RakNet.GetBuffer(binary, offset, 2));
                offset += 2;
                packet.splitIndex = Packet.BytesToInt(RakNet.GetBuffer(binary, offset, 4));
                offset += 4;
            }

            length = RakNet.GetBuffer(binary, offset).Length;

            packet.buffer = RakNet.GetBuffer(binary, offset, length);
            offset += length;

            return packet;
        }

        public int GetTotalLength()
        {
            return 3 + buffer.Length + (messageIndex != -1 ? 3 : 0) + (orderIndex != -1 ? 4 : 0) + (hasSplit ? 10 : 0);
        }

        public byte[] ToBinary(bool isInternal = false)
        {
            var ms = new MemoryStream();
            var w = new BinaryWriter(ms);
            w.Write((byte)((reliability << 5) | (hasSplit ? Convert.ToByte("00010000", 2) : 0x00)));
            if (isInternal)
            {
                w.Write(buffer.Length);
                w.Write(identifierACK);
            }
            else
            {
                w.Write(Packet.SwapUInt16((ushort)(buffer.Length*8)));
            }

            if (reliability > PacketReliability.UNRELIABLE)
            {
                if (reliability >= PacketReliability.RELIABLE && reliability != PacketReliability.UNRELIABLE_WITH_ACK_RECEIPT)
                {
                    w.Write(Packet.Int32ToLTriad(messageIndex));
                }
                if (reliability <= PacketReliability.RELIABLE_SEQUENCED && reliability != PacketReliability.RELIABLE)
                {
                    w.Write(Packet.Int32ToLTriad(orderIndex));
                    w.Write((byte)orderChannel);
                }
            }

            if (hasSplit)
            {
                w.Write(splitCount);
                w.Write((short)splitID);
                w.Write(splitIndex);
            }

            w.Write(buffer);

            return ms.ToArray();
        }

        public EncapsulatedPacket Clone()
        {
            var clone = new EncapsulatedPacket();
            clone.reliability = reliability;
            clone.hasSplit = hasSplit;
            clone.length = length;
            clone.messageIndex = messageIndex;
            clone.orderIndex = orderIndex;
            clone.orderChannel = orderChannel;
            clone.splitCount = splitCount;
            clone.splitID = splitID;
            clone.splitIndex = splitIndex;
            clone.buffer = buffer;
            clone.needACK = needACK;
            clone.identifierACK = identifierACK;

            return clone;
        }
    }
}
