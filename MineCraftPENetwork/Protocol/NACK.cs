using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineCraftPENetwork.Protocol
{
    public class NACK : AcknowledgePacket
    {
        public new const int ID = 0xA0;

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
