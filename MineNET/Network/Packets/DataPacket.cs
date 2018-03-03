using MineNET.Utils;
using System;

namespace MineNET.Network.Packets
{
    public abstract class DataPacket : MCBEBinary, ICloneable
    {
        public abstract byte PacketID
        {
            get;
        }

        public byte Extra1 { get; set; }

        public byte Extra2 { get; set; }

        public virtual void Encode()
        {
            this.WriteByte(this.PacketID);
            this.WriteByte(this.Extra1);
            this.WriteByte(this.Extra2);
        }

        public virtual void Decode()
        {
            this.ReadByte();
            this.Extra1 = this.ReadByte();
            this.Extra2 = this.ReadByte();
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
