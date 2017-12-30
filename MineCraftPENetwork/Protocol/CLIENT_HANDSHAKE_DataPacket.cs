using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineCraftPENetwork.Protocol
{
    public class CLIENT_HANDSHAKE_DataPacket : Packet
    {
        public new const int ID = 0x13;

        public string address;
        public int port;

        public string[] systemAddresses;

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

        public override void Decode()
        {
            base.Decode();

            var list = new List<string>();

            ReadAddress(ref address, ref port);
            for(int i = 0; i < 10; i++)
            {
                var str1 = "";
                var str2 = 0;
                ReadAddress(ref str1, ref str2);
                list.Add(str1 + ":" + str2);
            }
            systemAddresses = list.ToArray();
            sendPing = reader.ReadInt64();
            sendPong = reader.ReadInt64();
        }
    }
}
