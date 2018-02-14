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
            WriteByte(PacketID);
        }

        public virtual void Decode()
        {
            ReadByte();
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
