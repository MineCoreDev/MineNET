using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MineCraftPENetwork.Packets.RakNet.PacketCapsule
{
    public class PacketCapsule_0 : PacketCapsuleBase
    {
        public const byte CapsuleID = 0x00;

        public override byte GetID()
        {
            return CapsuleID;
        }
    }
}
