using System;
using MineNET.Utils;

namespace MineNET.Network.Packets
{
    public abstract class DataPacket : BinaryStream, ICloneable
    {
        public abstract byte PacketID
        {
            get;
        }

        byte e1;
        public byte Extra1
        {
            get
            {
                return e1;
            }

            set
            {
                e1 = value;
            }
        }
        byte e2;
        public byte Extra2
        {
            get
            {
                return e2;
            }

            set
            {
                e2 = value;
            }
        }

        public virtual void Encode()
        {
            WriteByte(PacketID);
            WriteByte(e1);
            WriteByte(e2);
        }

        public virtual void Decode()
        {
            ReadByte();
            e1 = ReadByte();
            e2 = ReadByte();
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
