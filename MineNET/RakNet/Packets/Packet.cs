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

        public long SendTime { get; set; }

        public virtual void Encode()
        {
            WriteByte(this.PacketID);
        }

        public virtual void Decode()
        {
            ReadByte();
        }

        public new Packet Clone()
        {
            return (Packet) this.MemberwiseClone();
        }

        object ICloneable.Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
