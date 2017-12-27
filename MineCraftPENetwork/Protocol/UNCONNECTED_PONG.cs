using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineCraftPENetwork.Protocol
{
    class UNCONNECTED_PONG : OfflineMessage
    {
        public new const byte ID = 0x1C;

        public long pingID;
        public long serverID;
        public string serverName;

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

            writer.Write(pingID);
            writer.Write(serverID);
            WriteMagic();
            WriteFixedString(writer, serverName);
        }

        public override void Decode()
        {
            base.Decode();

            pingID = reader.ReadInt64();
            serverID = reader.ReadInt64();
            ReadMagic();
            serverName = ReadFixedString();
        }
    }
}
