using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineCraftPENetwork.Protocol
{
    public class UNCONNECTED_PING : OfflineMessage
    {
        public new const byte ID = 0x01;

        public long pingID;

        public override byte PacketID
        {
            get
            {
                return ID;
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override void Encode()
        {
            base.Encode();

            writer.Write(pingID);
            WriteMagic();
        }

        public override void Decode()
        {
            base.Decode();

            pingID = reader.ReadInt64();
            ReadMagic();
        }
    }
}
