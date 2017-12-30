using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineCraftPENetwork.Protocol
{
    public class OPEN_CONNECTION_REPLY_1 : OfflineMessage
    {
        public new const byte ID = 0x06;

        public long serverID;
        public int mtuSize;

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
            writer.Write((byte)0);
            writer.Write((short)mtuSize);
        }

        public override void Decode()
        {
            base.Decode();

            ReadMagic();
            serverID = reader.ReadInt64();
            reader.ReadByte();
            mtuSize = reader.ReadInt16();
        }
    }
}
