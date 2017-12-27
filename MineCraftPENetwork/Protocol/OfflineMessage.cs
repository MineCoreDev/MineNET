using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MineCraftPENetwork.Protocol
{
    public abstract class OfflineMessage : Packet
    {
        protected byte[] magic;

        protected void ReadMagic()
        {
            magic = reader.ReadBytes(16);
        }

        protected void WriteMagic()
        {
            writer.Write(RakNet.MAGIC.ToArray());
        }

        public bool IsValid()
        {
            return magic.SequenceEqual(RakNet.MAGIC.ToArray());
        }
	}
}
