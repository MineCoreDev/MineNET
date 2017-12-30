using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineCraftPENetwork.Protocol
{
    public class DATA_PACKET_2 : DataPacket
    {
        public new const int ID = 0x82;

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
    }
}
