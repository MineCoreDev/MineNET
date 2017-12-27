using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineCraftPENetwork.Protocol
{
    class UNCONNECTED_PING_OPEN_CONNECTIONS : UNCONNECTED_PING
    {
        public new const byte ID = 0x02;

        public override byte PacketID
        {
            get
            {
                return ID;
            }
        }
    }
}
