using MineNET.Utils;
using System;

namespace MineNET.Network.RakNetPackets
{
    public abstract class RakNetPacket : BinaryStream, ICloneable<RakNetPacket>
    {
        public abstract byte MessageID { get; }

        public virtual void Encode()
        {
            this.WriteByte(this.MessageID);
        }
        public virtual void Decode()
        {
            this.ReadByte();
        }

        public new RakNetPacket Clone()
        {
            return (RakNetPacket) this.MemberwiseClone();
        }
    }
}
