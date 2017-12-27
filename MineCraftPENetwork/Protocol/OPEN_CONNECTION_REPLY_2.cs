using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineCraftPENetwork.Protocol
{
    class OPEN_CONNECTION_REPLY_2 : OfflineMessage
    {
        public new const byte ID = 0x08;

        public long serverID;
        public string clientAddress;
        public int clientPort;
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
            writer.Write(serverID);
            WriteAddress(clientAddress, clientPort, 4);
            writer.Write(mtuSize);
            writer.Write((byte)0);
        }

        public override void Decode()
        {
            base.Decode();

            ReadMagic();
            serverID = reader.Read();
            ReadAddress(ref clientAddress, ref clientPort);
            mtuSize = reader.ReadInt16();
            reader.ReadByte();
        }
    }
}
