using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineCraftPENetwork.Protocol
{
    public class SERVER_HANDSHAKE_DataPacket : Packet
    {
        public new const int ID = 0x10;

        public string address;
        public int port;
        public string[] systemAddresses = new string[]
        {
            "127.0.0.1",
            "0.0.0.0",
            "0.0.0.0",
            "0.0.0.0",
            "0.0.0.0",
            "0.0.0.0",
            "0.0.0.0",
            "0.0.0.0",
            "0.0.0.0",
            "0.0.0.0"
        };

        public long sendPing;
        public long sendPong;

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
            
            WriteAddress(address, port, 4);
            writer.Write((short)0);

            for (int i = 0; i < 10; ++i)
            {
                WriteAddress(systemAddresses[i], 0, 4);
            }

            writer.Write(sendPing);
            writer.Write(sendPong);
        }
    }
}
