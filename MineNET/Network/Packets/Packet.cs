using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MineNET.Utils;

namespace MineNET.Network.Packets
{
    public abstract class Packet : BinaryStream, ICloneable
    {
        public byte e1;
        public byte e2;

        public Packet()
        {

        }

        public Packet(byte[] buffer) : base(buffer)
        {
            
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public abstract byte ID
        {
            get;
        }

        public string Name
        {
            get
            {
                return this.GetType().Name;
            }
        }

        public virtual void Encode()
        {
            this.PutByte(e1);
            this.PutByte(e2);
        }

        public virtual void Decode()
        {
            e1 = this.ReadByte();
            e2 = this.ReadByte();
        }

        public virtual void Clear()
        {
            this.Flush();
        }

        public virtual void Reset()
        {
            this.Position = 0;
        }
    }
}
