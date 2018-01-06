using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Network.Packets
{
    public class UnknownPacket : Packet
    {
        public override byte ID
        {
            get
            {
                return 0;
            }
        }
    }
}
