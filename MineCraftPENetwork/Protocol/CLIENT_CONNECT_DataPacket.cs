using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineCraftPENetwork.Protocol
{
    public class CLIENT_CONNECT_DataPacket : Packet
    {
        public new const int ID = 0x09;

        public long clientID;
        public long sendPing;
        public bool useSecurity = false;

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

            writer.Write(clientID);
            writer.Write(sendPing);
            writer.Write(useSecurity ? (byte)1 : (byte)0);
        }

        public override void Decode()
        {
            base.Decode();

            clientID = reader.ReadInt64();
            sendPing = reader.ReadInt64();
            useSecurity = reader.ReadByte() > 0;
        }
    }
}
