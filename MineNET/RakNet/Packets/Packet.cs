using System;

using MineNET.Utils;

namespace MineNET.RakNet.Packets
{
    public abstract class Packet : BinaryStream, ICloneable<Packet>
    {
        public abstract byte PacketID
        {
            get;
        }

        public virtual void Encode()
        {
            WriteByte(this.PacketID);
        }

        public virtual void Decode()
        {
            ReadByte();
        }

        public virtual Packet Clone()
        {
            return (Packet) this.MemberwiseClone();
        }

        object ICloneable.Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
