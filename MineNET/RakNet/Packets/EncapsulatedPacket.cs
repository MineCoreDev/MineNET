using System;

using MineNET.Utils;

namespace MineNET.RakNet.Packets
{
    public class EncapsulatedPacket : ICloneable<EncapsulatedPacket>
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
            stream.WriteByte((byte) (this.reliability << 5 | (this.hasSplit ? Convert.ToByte("00010000", 2) : 0x00)));
            if (internalCall)
            {
                stream.WriteInt(this.buffer.Length);
                stream.WriteInt(this.identifierACK);
            }
            else
            {
                stream.WriteLShort((ushort) (this.buffer.Length * 8));
            }

            if (this.reliability > PacketReliability.UNRELIABLE)
            {
                if (this.reliability >= PacketReliability.RELIABLE && this.reliability != PacketReliability.UNRELIABLE_WITH_ACK_RECEIPT)
                {
                    stream.WriteLTriad(this.messageIndex);
                }
                if (this.reliability <= PacketReliability.RELIABLE_SEQUENCED && this.reliability != PacketReliability.RELIABLE)
                {
                    stream.WriteLTriad(this.orderIndex);
                    stream.WriteByte((byte) this.orderChannel);
                }
            }

            if (this.hasSplit)
            {
                stream.WriteInt(this.splitCount);
                stream.WriteShort((short) this.splitID);
                stream.WriteInt(this.splitIndex);
            }

            stream.WriteBytes(this.buffer);

            return stream.GetResult();
        }

        public EncapsulatedPacket Clone()
        {
            return (EncapsulatedPacket) this.MemberwiseClone();
        }

        object ICloneable.Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
