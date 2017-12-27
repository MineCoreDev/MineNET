using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineCraftPENetwork.Protocol
{
    class OPEN_CONNECTION_REQUEST_1 : OfflineMessage
    {
        public new const byte ID = 0x05;

        public byte protocol;
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
            writer.Write(protocol);
            writer.Write(new byte[mtuSize - 18]);
        }

        public override void Decode()
        {
            base.Decode();

            ReadMagic();
            protocol = reader.ReadByte();
            mtuSize = (int)(reader.BaseStream.Length - reader.BaseStream.Position) + 18;
        }
    }
}
