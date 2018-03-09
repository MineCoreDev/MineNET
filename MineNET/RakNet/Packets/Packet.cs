using System;

using MineNET.Utils;

namespace MineNET.RakNet.Packets
{
    public abstract class Packet : BinaryStream, ICloneable
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

        public new object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
