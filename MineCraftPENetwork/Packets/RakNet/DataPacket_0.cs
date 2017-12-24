using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MineCraftPENetwork.Packets.RakNet
{
    public class DataPacket_0 : DataPacket
    {
        public new const byte ID = 0x80;

        public override byte GetID()
        {
            return ID;
        }
    }
}
