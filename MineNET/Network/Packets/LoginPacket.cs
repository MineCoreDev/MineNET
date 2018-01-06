using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Network.Packets
{
    public class LoginPacket : Packet
    {
        public const byte NETWORK_ID = 0x01;

        private int protocol;

        public override byte ID
        {
            get
            {
                return NETWORK_ID;
            }
        }

        public override void Decode()
        {
            this.Reset();
            base.Decode();
            protocol = this.ReadInt();
            Console.WriteLine(protocol);
            /*for (int i = 0; i < 10; ++i)
            {
                Console.WriteLine(this.ReadByte());
            }*/
        }
    }
}
