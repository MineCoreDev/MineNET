using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MineCraftPENetwork.Packets.RakNet.CapsuledPacket
{
    class ClientHandShakePacket : RakNetPacket
    {
        public const byte ID = 0x13;

        public string ip;
        public short port;
        
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
            BinaryReader br = reader;

            ReadIPPort(reader, ref ip, ref port);
            List<string> list = new List<string>();
            for(int i = 0; i < 10; i++)
            {
                list.Add(ReadIPPort(reader));
            }
            ipList = list.ToArray();

            session = br.ReadInt64();
            pong = br.ReadInt64();
        }

        public override byte[] Encode()
        {
            throw new NotImplementedException();
        }
    }
}
