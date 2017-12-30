using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineCraftPENetwork.Protocol
{
    public class DATA_PACKET_1 : DataPacket
    {
        public new const int ID = 0x81;

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
