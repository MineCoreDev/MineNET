using System;

using MineNET.Utils;

namespace MineNET.RakNet.Packets
{
    public class EncapsulatedPacket : ICloneable
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

        public static EncapsulatedPacket ToEncapsulatedPacket(ref byte[] buffer, bool internalCall = false)
        {
            EncapsulatedPacket pk = new EncapsulatedPacket();
            BinaryStream stream = new BinaryStream(buffer);

            int flags = stream.ReadByte() & 0xff;
            int rb = ((flags & Convert.ToByte("01110000", 2)) >> 5);
            bool hs = ((flags & Convert.ToByte("00010000", 2)) > 0);
            int length = 0;

            pk.reliability = rb;
            pk.hasSplit = hs;

            if (internalCall)
            {
                length = stream.ReadInt();
                pk.identifierACK = stream.ReadInt();
            }
            else
            {
                length = stream.ReadLShort();
            }

            if (rb > PacketReliability.UNRELIABLE)
            {
                if (rb >= PacketReliability.RELIABLE && rb != PacketReliability.UNRELIABLE_WITH_ACK_RECEIPT)
                {
                    pk.messageIndex = stream.ReadLTriad();
                }

                if (rb <= PacketReliability.RELIABLE_SEQUENCED && rb != PacketReliability.RELIABLE)
                {
                    pk.orderIndex = stream.ReadLTriad();
                    pk.orderChannel = stream.ReadByte();
                }
            }

            if (hs)
            {
                pk.splitCount = stream.ReadInt();
                pk.splitID = stream.ReadShort();
                pk.splitIndex = stream.ReadInt();
            }

            pk.buffer = stream.ReadBytes();
            pk.length = pk.buffer.Length;
            buffer = stream.ReadBytes();

            return pk;
        }

        public byte[] ToResult(bool internalCall = false)
        {
            BinaryStream stream = new BinaryStream();
            stream.WriteByte((byte)(reliability << 5 | (hasSplit ? Convert.ToByte("00010000", 2) : 0x00)));
            if (internalCall)
            {
                stream.WriteInt(buffer.Length);
                stream.WriteInt(identifierACK);
            }
            else
            {
                stream.WriteLShort((ushort)(buffer.Length * 8));
            }

            if (reliability > PacketReliability.UNRELIABLE)
            {
                if (reliability >= PacketReliability.RELIABLE && reliability != PacketReliability.UNRELIABLE_WITH_ACK_RECEIPT)
                {
                    stream.WriteLTriad(messageIndex);
                }
                if (reliability <= PacketReliability.RELIABLE_SEQUENCED && reliability != PacketReliability.RELIABLE)
                {
                    stream.WriteLTriad(orderIndex);
                    stream.WriteByte((byte)orderChannel);
                }
            }

            if (hasSplit)
            {
                stream.WriteInt(splitCount);
                stream.WriteShort((short)splitID);
                stream.WriteInt(splitIndex);
            }

            stream.WriteBytes(buffer);

            return stream.GetResult();
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
