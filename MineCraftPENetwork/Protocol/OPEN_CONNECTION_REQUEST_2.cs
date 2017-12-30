using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineCraftPENetwork.Protocol
{
    public class OPEN_CONNECTION_REQUEST_2 : OfflineMessage
    {
        public new const byte ID = 0x07;

        public long clientID;
	    public string serverAddress;
	    public int serverPort;
	    public short mtuSize;

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

            WriteMagic();
            WriteAddress(serverAddress, serverPort, 4);
            writer.Write(mtuSize);
            writer.Write(clientID);
        }

        public override void Decode()
        {
            base.Decode();

            ReadMagic();
            ReadAddress(ref serverAddress, ref serverPort);
            mtuSize = reader.ReadInt16();
            clientID = reader.ReadInt64();
        }
    }
}
