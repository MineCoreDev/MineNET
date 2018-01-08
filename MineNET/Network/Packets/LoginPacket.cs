using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

using MineNET.Utils;
using Newtonsoft.Json.Linq;

namespace MineNET.Network.Packets
{
    public class LoginPacket : Packet
    {
        public const byte NETWORK_ID = 0x01;

        private int protocol;
        private string chainData;

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

            var bs = new BinaryStream(this.ReadPacketBuffer());
            bs.Position = 0;

            chainData = Encoding.UTF8.GetString(bs.ReadBytes(4, (int)bs.ReadLInt() + 4));
            var obj = JObject.Parse(chainData)["chain"];//TODO JWT Decode...
            foreach (var s in obj)
            {
                Console.WriteLine(JObject.Parse(JWT.Decode(s.ToString())).ToString());
            }
        }
    }
}
