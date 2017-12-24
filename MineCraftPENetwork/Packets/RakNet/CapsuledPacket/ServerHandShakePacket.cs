using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MineCraftPENetwork.Packets.RakNet.CapsuledPacket
{
    class ServerHandShakePacket : RakNetPacket
    {
        public const byte ID = 0x10;

        public string ip;
        public short port;

        public short index = 0;
        public string[] ipList = new string[]
        {
            "127.0.0.1:0",
            "0.0.0.0:0",
            "0.0.0.0:0",
            "0.0.0.0:0",
            "0.0.0.0:0",
            "0.0.0.0:0",
            "0.0.0.0:0",
            "0.0.0.0:0",
            "0.0.0.0:0",
            "0.0.0.0:0"
        };

        public long session;
        public long pong;

        public override void Decode(BinaryReader reader)
        {
            throw new NotImplementedException();
        }

        public override byte[] Encode()
        {
            MemoryStream ms = new MemoryStream();
            BinaryWriter bw = new BinaryWriter(ms);

            bw.Write(ID);
            WriteIPPort(bw, ip, port);
            bw.Write(index);
            foreach(var ipData in ipList)
            {
                var sp = ipData.Split(':');
                WriteIPPort(bw, sp[0], short.Parse(sp[1]));
            }
            bw.Write(session);
            bw.Write(pong);

            return ms.ToArray();
        }
    }
}
