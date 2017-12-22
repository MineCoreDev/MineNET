using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MineCraftPENetwork.Packets.MCPE
{
    public class MCPEPacket : Packet
    {
        public static BinaryReader ConvertBinaryReader(byte[] buffer)
        {
            MemoryStream ms = new MemoryStream(buffer);
            BinaryReader br = new BinaryReader(ms);

            return br;
        }
    }
}
