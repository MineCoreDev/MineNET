using System;
using MineNET.Utils;
using MineNET.Values;

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

        public Vector2 ReadVector2()
        {
            return MCPEBinary.ReadVector2(this);
        }

        public void WriteVector2(Vector2 value)
        {
            MCPEBinary.WriteVector2(this, value);
        }

        public Vector3 ReadVector3()
        {
            return MCPEBinary.ReadVector3(this);
        }

        public void WriteVector3(Vector3 value)
        {
            MCPEBinary.WriteVector3(this, value);
        }
    }
}
