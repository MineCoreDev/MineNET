﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineCraftPENetwork.Protocol
{
    public class PING_DataPacket : Packet
    {
        public new const int ID = 0x00;

        public long pingID;

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
        }

        public override void Decode()
        {
            base.Decode();

           pingID = reader.ReadInt64();
        }
    }
}
