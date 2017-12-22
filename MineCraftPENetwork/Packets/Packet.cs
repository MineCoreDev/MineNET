using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MineCraftPENetwork.Packets
{
    public abstract class Packet
    {
        public virtual byte[] Encode()
        {
            throw new NotImplementedException();
        }

        public virtual void Decode(BinaryReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
