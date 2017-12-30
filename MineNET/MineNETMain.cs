using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using MineCraftPENetwork.Server;
using MineCraftPENetwork.Protocol;

using MineNET.Utils;

namespace MineNET
{
    public class MineNETMain
    {
        public MainLogger logger;

        public RakNetServer server;

        public Timer serverNameUpdater;
        public Timer serverHandler;

        public MineNETMain()
        {
            logger = new MainLogger();

            server = new RakNetServer(1115);

            var h = new ServerHandler(server, new MineNETServerHandler());

            serverNameUpdater = new Timer((obj) =>
            {
                h.SendOption("name", Encoding.UTF8.GetBytes("MCPE;MineNETServer;160;1.2.8;0;20;MineNET;Survival"));
            }, null, 100, 1000);

            serverHandler = new Timer((obj) =>
            {
                h.HandlePacket();
            }, null, 0, 50);
        }

        class MineNETServerHandler : ServerInstance
        {
            public void CloseSession(string identifier, string reason)
            {
                //throw new NotImplementedException();
            }

            public void HandleEncapsulated(string identifier, EncapsulatedPacket packet, int flags)
            {
                try
                {
                    var pk = GetPacket(packet.buffer);
                }
                catch (Exception e)
                {

                }
            }

            public void HandleOption(string option, byte[] value)
            {
                //throw new NotImplementedException();
            }

            public void HandleRaw(string address, int port, byte[] payload)
            {
                //throw new NotImplementedException();
            }

            public void NotifyACK(string identifier, int identifierACK)
            {
                //throw new NotImplementedException();
            }

            public void OpenSession(string identifier, string address, int port, long clientID)
            {
                //throw new NotImplementedException();
            }

            public byte[] GetPacket(byte[] buffer)
            {
                var pid = buffer[0];
                var ms = new System.IO.MemoryStream(MineCraftPENetwork.RakNet.GetBuffer(buffer, 1));
                var br = new System.IO.BinaryReader(ms);
                
                return ms.ToArray();
            }
        }
    }
}
