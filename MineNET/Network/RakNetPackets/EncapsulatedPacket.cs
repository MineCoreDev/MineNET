using System;
using MineNET.Utils;

namespace MineNET.Network.RakNetPackets
{
    public class EncapsulatedPacket : ICloneable<EncapsulatedPacket>
    {
        public int Reliability { get; set; }
        public bool HasSplit { get; set; } = false;
        public int Length { get; set; } = 0;
        public int MessageIndex { get; set; } = -1;
        public int OrderIndex { get; set; } = -1;
        public int OrderChannel { get; set; } = 0;
        public int SplitCount { get; set; } = 0;
        public int SplitID { get; set; } = 0;
        public int SplitIndex { get; set; } = 0;
        public byte[] Buffer { get; set; }
        public bool NeedACK { get; set; } = false;
        public int IdentifierACK { get; set; } = -1;

        public static EncapsulatedPacket ToEncapsulatedPacket(ref byte[] buffer, bool internalCall = false)
        {
            EncapsulatedPacket pk = new EncapsulatedPacket();
            BinaryStream stream = new BinaryStream(buffer);

            int flags = stream.ReadByte() & 0xff;
            int rb = ((flags & Convert.ToByte("01110000", 2)) >> 5);
            bool hs = ((flags & Convert.ToByte("00010000", 2)) > 0);
            int length = 0;

            pk.Reliability = rb;
            pk.HasSplit = hs;

            if (internalCall)
            {
                length = stream.ReadInt();
                pk.IdentifierACK = stream.ReadInt();
            }
            else
            {
                length = stream.ReadUShort() / 8;
            }

            if (rb > RakNetPacketReliability.UNRELIABLE)
            {
                if (rb >= RakNetPacketReliability.RELIABLE && rb != RakNetPacketReliability.UNRELIABLE_WITH_ACK_RECEIPT)
                {
                    pk.MessageIndex = stream.ReadLTriad();
                }

                if (rb <= RakNetPacketReliability.RELIABLE_SEQUENCED && rb != RakNetPacketReliability.RELIABLE)
                {
                    pk.OrderIndex = stream.ReadLTriad();
                    pk.OrderChannel = stream.ReadByte();
                }
            }

            if (hs)
            {
                pk.SplitCount = stream.ReadInt();
                pk.SplitID = stream.ReadShort();
                pk.SplitIndex = stream.ReadInt();
            }

            pk.Buffer = stream.ReadBytes(length);
            pk.Length = pk.Buffer.Length;

            buffer = stream.ReadBytes();

            return pk;
        }

        public int GetTotalLength()
        {
            return 3 + this.Buffer.Length + (this.MessageIndex != -1 ? 3 : 0) + (this.OrderIndex != -1 ? 4 : 0) +
                   (this.HasSplit ? 10 : 0);
        }

        public byte[] ToResult(bool internalCall = false)
        {
            BinaryStream stream = new BinaryStream();
            stream.WriteByte((byte) (this.Reliability << 5 | (this.HasSplit ? Convert.ToByte("00010000", 2) : 0x00)));
            if (internalCall)
            {
                stream.WriteInt(this.Buffer.Length);
                stream.WriteInt(this.IdentifierACK);
            }
            else
            {
                stream.WriteUShort((ushort) (this.Buffer.Length * 8));
            }

            if (this.Reliability > RakNetPacketReliability.UNRELIABLE)
            {
                if (this.Reliability >= RakNetPacketReliability.RELIABLE &&
                    this.Reliability != RakNetPacketReliability.UNRELIABLE_WITH_ACK_RECEIPT)
                {
                    stream.WriteLTriad(this.MessageIndex);
                }

                if (this.Reliability <= RakNetPacketReliability.RELIABLE_SEQUENCED &&
                    this.Reliability != RakNetPacketReliability.RELIABLE)
                {
                    stream.WriteLTriad(this.OrderIndex);
                    stream.WriteByte((byte) this.OrderChannel);
                }
            }

            if (this.HasSplit)
            {
                stream.WriteInt(this.SplitCount);
                stream.WriteShort((short) this.SplitID);
                stream.WriteInt(this.SplitIndex);
            }

            stream.WriteBytes(this.Buffer);

            return stream.ToArray();
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